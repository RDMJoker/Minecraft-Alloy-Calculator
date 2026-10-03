namespace Minecraft_Alloy_Calculator;

public readonly record struct IngredientRange(Alloy Ingredient, int MinUnits, int MaxUnits);

public sealed class AlloyResolution
{
    public Alloy Alloy { get; init; } = null!;
    public int RequestedAmount { get; init; }
    public int ActualAmount { get; init; }
    public Dictionary<Alloy, AlloyResolution> Ingredients { get; init; } = [];
    public Dictionary<EAlloyName, int> RawMaterials { get; init; } = [];
    public double Score { get; init; }
}

public static class AlloyMath
{
    const int Unit = 16;
    const int PreferredMultiple = 144;
    const int PreferredUnits = PreferredMultiple / Unit;
    const int MaxAmountAdjustment = 100;
    const double AmountAdjustmentWeight = 1000.0;
    const double Non144Weight = 10.0;
    const double UnitCountWeight = 0.01;
    const double RangeDeviationWeight = 1.0;
    static readonly Dictionary<(EAlloyName Alloy, int Amount), AlloyResolution?> cache = [];

    public static AlloyResolution? ResolveAlloy(Alloy _alloy, int _requestedAmount)
    {
        cache.Clear();
        return ResolveAlloyInternal(_alloy, _requestedAmount);
    }

    static AlloyResolution? ResolveAlloyInternal(Alloy _alloy, int _requestedAmount)
    {
        if (_alloy.AlloyInformation.Count == 0)
            return new AlloyResolution
            {
                Alloy = _alloy,
                RequestedAmount = _requestedAmount,
                ActualAmount = _requestedAmount,
                RawMaterials = new Dictionary<EAlloyName, int> { { _alloy.Name, _requestedAmount } },
                Score = 0
            };
        var cacheKey = (_alloy.Name, requestedAmount: _requestedAmount);
        if (cache.TryGetValue(cacheKey, out var cached))
            return cached;
        AlloyResolution? bestResolution = null;
        int requestedUnits = (int)Math.Ceiling(_requestedAmount / (double)Unit);
        int ratioMultiplier = _alloy.RecipeType == EAlloyRecipeType.Ingot ? GetRatioMultiplier(_alloy) : 1;
        for (int adjustment = 0; adjustment <= MaxAmountAdjustment; adjustment++)
        {
            int outputUnits = requestedUnits + adjustment;
            int totalUnits = outputUnits * ratioMultiplier;
            int actualAmount = outputUnits * Unit;
            var candidate = FindBestRatioForAmount(_alloy, actualAmount, totalUnits);
            if (candidate == null)
                continue;
            if (bestResolution == null || candidate.Score < bestResolution.Score)
                bestResolution = candidate;
            if (adjustment > 0 && bestResolution != null && bestResolution.Score < adjustment * AmountAdjustmentWeight)
                break;
        }
        cache[cacheKey] = bestResolution;
        return bestResolution;
    }
    
    private static int GetRatioInputUnits(Alloy _alloy, int _requestedAmount)
    {
        int outputUnits = (_requestedAmount + Unit - 1) / Unit;
        return outputUnits * GetRatioMultiplier(_alloy);
    }
    
    private static int GetRatioMultiplier(Alloy _alloy)
    {
        return (int)Math.Ceiling(_alloy.AlloyInformation.Values.Sum(_range => _range[0]));
    }

    static AlloyResolution? FindBestRatioForAmount(Alloy _alloy, int _requestedAmount, int _totalUnits)
    {
        var ranges = CreateRanges(_alloy, _totalUnits);
        int minimumTotal = ranges.Sum(_x => _x.MinUnits);
        int maximumTotal = ranges.Sum(_x => _x.MaxUnits);
        if (_totalUnits < minimumTotal || _totalUnits > maximumTotal)
            return null;
        int[] amounts = new int[ranges.Length];
        AlloyResolution? bestResolution = null;
        SearchRatio(_alloy, _requestedAmount, ranges, 0, _totalUnits, amounts, ref bestResolution);
        return bestResolution;
    }

    static void SearchRatio(Alloy _alloy, int _requestedAmount, IngredientRange[] _ranges, int _index, int _remainingUnits, int[] _currentAmounts, ref AlloyResolution? _bestResolution)
    {
        if (_index == _ranges.Length - 1)
        {
            var last = _ranges[_index];
            if (_remainingUnits < last.MinUnits || _remainingUnits > last.MaxUnits)
                return;
            _currentAmounts[_index] = _remainingUnits;
            var resolution = BuildResolution(_alloy, _requestedAmount, _ranges, _currentAmounts);
            if (resolution != null && (_bestResolution == null || resolution.Score < _bestResolution.Score))
                _bestResolution = resolution;
            return;
        }

        int remainingMin = 0;
        int remainingMax = 0;
        for (int i = _index + 1; i < _ranges.Length; i++)
        {
            remainingMin += _ranges[i].MinUnits;
            remainingMax += _ranges[i].MaxUnits;
        }

        foreach (int amount in GetCandidateOrder(_ranges[_index].MinUnits, _ranges[_index].MaxUnits))
        {
            int newRemaining = _remainingUnits - amount;
            if (newRemaining < remainingMin || newRemaining > remainingMax)
                continue;
            _currentAmounts[_index] = amount;
            SearchRatio(_alloy, _requestedAmount, _ranges, _index + 1, newRemaining, _currentAmounts, ref _bestResolution);
        }
    }

    static AlloyResolution? BuildResolution(Alloy _alloy, int _requestedAmount, IngredientRange[] _ranges, int[] _amounts)
    {
        var ingredients = new Dictionary<Alloy, AlloyResolution>();
        var rawMaterials = new Dictionary<EAlloyName, int>();
        double score = 0;
        var ingredientList = _alloy.AlloyInformation.Keys.ToList();
        for (int i = 0; i < ingredientList.Count; i++)
        {
            Alloy ingredient = ingredientList[i];
            int amount = _amounts[i] * Unit;
            if (ingredient.AlloyInformation.Count == 0)
            {
                var rawResolution = new AlloyResolution { Alloy = ingredient, RequestedAmount = amount, ActualAmount = amount, RawMaterials = new Dictionary<EAlloyName, int> { { ingredient.Name, amount } }, Score = 0 };
                ingredients[ingredient] = rawResolution;
                AddAmount(rawMaterials, ingredient.Name, amount);
                score += CalculateAmountScore(_ranges[i], _amounts[i]);
                continue;
            }
            AlloyResolution? childResolution = ResolveAlloyInternal(ingredient, amount);
            if (childResolution == null)
                return null;
            ingredients[ingredient] = childResolution;
            MergeAmounts(rawMaterials, childResolution.RawMaterials);
            score += childResolution.Score;
            score += CalculateAmountScore(_ranges[i], _amounts[i]);
        }
        int actualAmount = _alloy.RecipeType == EAlloyRecipeType.Ingot ? _requestedAmount : _amounts.Sum() * Unit;
        int adjustment = actualAmount - _requestedAmount;
        score += adjustment * AmountAdjustmentWeight;
        return new AlloyResolution { Alloy = _alloy, RequestedAmount = _requestedAmount, ActualAmount = actualAmount, Ingredients = ingredients, RawMaterials = rawMaterials, Score = score };
    }

    static IngredientRange[] CreateRanges(Alloy _alloy, int _totalUnits)
    {
        if (_alloy.RecipeType == EAlloyRecipeType.Ingot)
        {
            float ratioTotal = _alloy.AlloyInformation.Values.Sum(_x => _x[0]);
            return _alloy.AlloyInformation.Select(_entry => new IngredientRange(_entry.Key, (int)Math.Ceiling(_totalUnits * (_entry.Value[0] / ratioTotal)), (int)Math.Floor(_totalUnits * (_entry.Value[1] / ratioTotal)))).ToArray();
        }
        return _alloy.AlloyInformation.Select(_entry => new IngredientRange(_entry.Key, (int)Math.Ceiling(_totalUnits * _entry.Value[0]), (int)Math.Floor(_totalUnits * _entry.Value[1]))).ToArray();
    }

    static IEnumerable<int> GetCandidateOrder(int _min, int _max)
    {
        for (int amount = _min; amount <= _max; amount++)
            if (amount % PreferredUnits == 0)
                yield return amount;
        for (int amount = _min; amount <= _max; amount++)
            if (amount % PreferredUnits != 0)
                yield return amount;
    }

    static double CalculateAmountScore(IngredientRange _range, int _amount)
    {
        double score = 0;
        if (_amount % PreferredUnits != 0)
            score += Non144Weight;
        score += _amount * UnitCountWeight;
        int rangeSize = _range.MaxUnits - _range.MinUnits;
        if (rangeSize > 0)
        {
            double center = (_range.MinUnits + _range.MaxUnits) / 2.0;
            score += Math.Abs(_amount - center) / rangeSize * RangeDeviationWeight;
        }

        return score;
    }

    static void AddAmount(Dictionary<EAlloyName, int> _dictionary, EAlloyName _name, int _amount)
    {
        if (_dictionary.TryGetValue(_name, out int existing))
            _dictionary[_name] = existing + _amount;
        else
            _dictionary.Add(_name, _amount);
    }

    static void MergeAmounts(Dictionary<EAlloyName, int> _target, Dictionary<EAlloyName, int> _source)
    {
        foreach (var entry in _source)
            AddAmount(_target, entry.Key, entry.Value);
    }
}