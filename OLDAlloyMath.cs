namespace Minecraft_Alloy_Calculator;

//class AlloyMath
//{
//    public static List<int> CalcRawIngotAmount(Dictionary<Alloy, float[]> _alloyInformation, int _amount)
//    {
//        //Check how many Ingredients we have
//        int ingAmount = _alloyInformation.Count;
//        return ingAmount switch
//        {
//            2 => CalcRawIngotAmount2In(_alloyInformation, _amount),
//            3 => CalcRawIngotAmount3In(_alloyInformation, _amount),
//            4 => CalcRawIngotAmount4In(_alloyInformation, _amount),
//            _ => throw new NotImplementedException()
//        };
//    }
//
//    static List<int> CalcRawIngotAmount2In(Dictionary<Alloy, float[]> _alloyInformation, int _amount)
//    {
//        List<int> amountList = [];
//
//        //Pull ranges
//        List<float[]> rangeList = [];
//        rangeList.AddRange(_alloyInformation.Select(_entry => _entry.Value));
//
//        //Link ranges to variables
//        double ing1Min = _amount * rangeList[0][0];
//        double ing1Max = _amount * rangeList[0][1];
//        double ing2Min = _amount * rangeList[1][0];
//        double ing2Max = _amount * rangeList[1][1];
//
//        //Calculate initial values via midpoint
//        double ing1Amount = (ing1Min + ing1Max) / 2;
//        double ing2Amount = (ing2Min + ing2Max) / 2;
//
//        bool solutionFound = false;
//
//        FindAmount2Ing(_amount, ing1Min, ing1Max, ing2Min, ing2Max, ref ing1Amount, ref solutionFound, ref ing2Amount);
//        if (!solutionFound)
//        {
//            int newAmount = _amount;
//            for (int i = 0; i < 100; i++)
//            {
//                newAmount += 16;
//                ing1Min = newAmount * rangeList[0][0];
//                ing1Max = newAmount * rangeList[0][1];
//                ing2Min = newAmount * rangeList[1][0];
//                ing2Max = newAmount * rangeList[1][1];
//                FindAmount2Ing(newAmount, ing1Min, ing1Max, ing2Min, ing2Max, ref ing1Amount, ref solutionFound, ref ing2Amount, i);
//            }
//        }
//
//        amountList.Add((int)ing1Amount);
//        amountList.Add((int)ing2Amount);
//        return amountList;
//    }
//
//    static void FindAmount2Ing(int _amount, double _ing1Min, double _ing1Max, double _ing2Min, double _ing2Max, ref double _ing1Amount, ref bool _solutionFound, ref double _ing2Amount, int _counter = 0)
//    {
//        for (double y = _ing1Min; y < _ing1Max; y += 16)
//        {
//            double roundedY = RoundToNearestWithinRange(y, 144, 16, _ing1Min, _ing1Max);
//            double remainingZ = _amount - roundedY;
//            if (!(remainingZ >= _ing2Min) || !(remainingZ <= _ing2Max)) continue;
//            double roundedZ = RoundToNearestWithinRange(remainingZ, 144, 16, _ing2Min, _ing2Max);
//            if (Math.Abs(roundedY + roundedZ - _amount) < 0.0001)
//            {
//                _ing1Amount = roundedY;
//                _ing2Amount = roundedZ;
//                _solutionFound = true;
//                break;
//            }
//
//            if (_counter < 50) continue;
//            if (!(Math.Abs(roundedY + roundedZ - _amount) < 16) || Math.Abs(roundedY + roundedZ - _amount) < 0) continue;
//            _ing1Amount = roundedY;
//            _ing2Amount = roundedZ;
//            _solutionFound = true;
//            break;
//        }
//    }
//
//    static List<int> CalcRawIngotAmount3In(Dictionary<Alloy, float[]> _alloyInformation, int _amount)
//    {
//        List<int> amountList = [];
//        //Pull ranges
//        List<float[]> rangeList = [];
//        rangeList.AddRange(_alloyInformation.Select(_entry => _entry.Value));
//
//        //Link ranges to variables
//        double ing1Min = _amount * rangeList[0][0];
//        double ing1Max = _amount * rangeList[0][1];
//        double ing2Min = _amount * rangeList[1][0];
//        double ing2Max = _amount * rangeList[1][1];
//        double ing3Min = _amount * rangeList[2][0];
//        double ing3Max = _amount * rangeList[2][1];
//
//        bool solutionFound = false;
//
//        double ing1Amount = 0, ing2Amount = 0, ing3Amount = 0;
//
//        FindAmount3Ing(_amount, ing1Min, ing1Max, ing2Min, ing2Max, ing3Min, ing3Max, ref ing1Amount, ref solutionFound, ref ing2Amount, ref ing3Amount);
//        if (!solutionFound)
//        {
//            int newAmount = _amount;
//            for (int i = 0; i < 100; i++)
//            {
//                newAmount += 16;
//                ing1Min = newAmount * rangeList[0][0];
//                ing1Max = newAmount * rangeList[0][1];
//                ing2Min = newAmount * rangeList[1][0];
//                ing2Max = newAmount * rangeList[1][1];
//                ing3Min = newAmount * rangeList[2][0];
//                ing3Max = newAmount * rangeList[2][1];
//                FindAmount3Ing(newAmount, ing1Min, ing1Max, ing2Min, ing2Max, ing3Min, ing3Max, ref ing1Amount, ref solutionFound, ref ing2Amount, ref ing3Amount, i);
//                if (solutionFound)
//                {
//                    break;
//                }
//            }
//        }
//
//        amountList.Add((int)ing1Amount);
//        amountList.Add((int)ing2Amount);
//        amountList.Add((int)ing3Amount);
//        return amountList;
//    }
//
//    static void FindAmount3Ing(int _amount, double _ing1Min, double _ing1Max, double _ing2Min, double _ing2Max, double _ing3Min, double _ing3Max, ref double _ing1Amount, ref bool _solutionFound, ref double _ing2Amount, ref double _ing3Amount, int _counter = 0)
//    {
//        for (double y = _ing1Min; y <= _ing1Max; y += 16)
//        {
//            double roundedY = RoundToNearestWithinRange(y, 144, 16, _ing1Min, _ing1Max);
//
//            for (double z = _ing2Min; z <= _ing2Max; z += 16)
//            {
//                double roundedZ = RoundToNearestWithinRange(z, 144, 16, _ing2Min, _ing2Max);
//
//                double remainingK = _amount - roundedY - roundedZ;
//                if (!(remainingK >= _ing3Min) || !(remainingK <= _ing3Max)) continue;
//                double roundedK = RoundToNearestWithinRange(remainingK, 144, 16, _ing3Min, _ing3Max);
//                if (Math.Abs(roundedY + roundedZ + roundedK - _amount) < 0.0001)
//                {
//                    _ing1Amount = roundedY;
//                    _ing2Amount = roundedZ;
//                    _ing3Amount = roundedK;
//                    _solutionFound = true;
//                    break;
//                }
//
//                if (_counter < 50) continue;
//                if (!(Math.Abs(roundedY + roundedZ + roundedK - _amount) < 16) || Math.Abs(roundedY + roundedZ + roundedK - _amount) < 0) continue;
//                _ing1Amount = roundedY;
//                _ing2Amount = roundedZ;
//                _ing3Amount = roundedK;
//                _solutionFound = true;
//                break;
//            }
//
//            if (_solutionFound) break;
//        }
//    }
//
//    static List<int> CalcRawIngotAmount4In(Dictionary<Alloy, float[]> _alloyInformation, int _amount)
//    {
//        List<int> amountList = [];
//
//        //Pull ranges
//        List<float[]> rangeList = [];
//        rangeList.AddRange(_alloyInformation.Select(_entry => _entry.Value));
//
//        //Link ranges to variables
//        double ing1Min = _amount * rangeList[0][0];
//        double ing1Max = _amount * rangeList[0][1];
//        double ing2Min = _amount * rangeList[1][0];
//        double ing2Max = _amount * rangeList[1][1];
//        double ing3Min = _amount * rangeList[2][0];
//        double ing3Max = _amount * rangeList[2][1];
//        double ing4Min = _amount * rangeList[3][0];
//        double ing4Max = _amount * rangeList[3][1];
//
//        bool solutionFound = false;
//
//        double ing1Amount = 0, ing2Amount = 0, ing3Amount = 0, ing4Amount = 0;
//
//        FindAmount4Ing(_amount, ing1Min, ing1Max, ing2Min, ing2Max, ing3Min, ing3Max, ing4Min, ing4Max, ref ing1Amount, ref solutionFound, ref ing2Amount, ref ing3Amount, ref ing4Amount);
//
//        if (!solutionFound)
//        {
//            int newAmount = _amount;
//            for (int i = 0; i < 100; i++)
//            {
//                newAmount += 16;
//                ing1Min = newAmount * rangeList[0][0];
//                ing1Max = newAmount * rangeList[0][1];
//                ing2Min = newAmount * rangeList[1][0];
//                ing2Max = newAmount * rangeList[1][1];
//                ing3Min = newAmount * rangeList[2][0];
//                ing3Max = newAmount * rangeList[2][1];
//                ing4Min = newAmount * rangeList[3][0];
//                ing4Max = newAmount * rangeList[3][1];
//                FindAmount4Ing(newAmount, ing1Min, ing1Max, ing2Min, ing2Max, ing3Min, ing3Max, ing4Min, ing4Max, ref ing1Amount, ref solutionFound, ref ing2Amount, ref ing3Amount, ref ing4Amount, i);
//                if (solutionFound)
//                {
//                    break;
//                }
//            }
//        }
//
//        amountList.Add((int)ing1Amount);
//        amountList.Add((int)ing2Amount);
//        amountList.Add((int)ing3Amount);
//        amountList.Add((int)ing4Amount);
//        return amountList;
//    }
//
//    static void FindAmount4Ing(int _amount, double _ing1Min, double _ing1Max, double _ing2Min, double _ing2Max, double _ing3Min, double _ing3Max, double _ing4Min, double _ing4Max, ref double _ing1Amount, ref bool _solutionFound, ref double _ing2Amount, ref double _ing3Amount, ref double _ing4Amount, int _counter = 0)
//    {
//        for (double y = _ing1Min; y <= _ing1Max; y += 16)
//        {
//            double roundedY = RoundToNearestWithinRange(y, 144, 16, _ing1Min, _ing1Max);
//
//            for (double z = _ing2Min; z <= _ing2Max; z += 16)
//            {
//                double roundedZ = RoundToNearestWithinRange(z, 144, 16, _ing2Min, _ing2Max);
//                for (double k = _ing3Min; k <= _ing3Max; k += 16)
//                {
//                    double roundedK = RoundToNearestWithinRange(k, 144, 16, _ing3Min, _ing3Max);
//
//                    double remainingW = _amount - roundedK - roundedZ - roundedY;
//                    if (!(remainingW >= _ing4Min) || !(remainingW <= _ing4Max)) continue;
//                    double roundedW = RoundToNearestWithinRange(remainingW, 144, 16, _ing4Min, _ing4Max);
//                    if (Math.Abs(roundedY + roundedZ + roundedK + roundedW - _amount) < 0.0001)
//                    {
//                        _ing1Amount = roundedY;
//                        _ing2Amount = roundedZ;
//                        _ing3Amount = roundedK;
//                        _ing4Amount = roundedW;
//                        _solutionFound = true;
//                        break;
//                    }
//
//                    if (_counter < 50) continue;
//                    if (!(Math.Abs(roundedY + roundedZ + roundedK + roundedW - _amount) < 16) || Math.Abs(roundedY + roundedZ + roundedK + roundedW - _amount) < 0) continue;
//                    _ing1Amount = roundedY;
//                    _ing2Amount = roundedZ;
//                    _ing3Amount = roundedK;
//                    _ing4Amount = roundedW;
//                    _solutionFound = true;
//                    break;
//                }
//            }
//
//            if (_solutionFound) break;
//        }
//    }
//
//    static double RoundToNearestWithinRange(double _value, int _primaryMultiple, int _fallbackMultiple, double _min, double _max)
//    {
//        // Try rounding to the primary multiple first (144)
//        double roundedValue = RoundToNearest(_value, _primaryMultiple);
//
//        // Check if it's within range; if not, round to the fallback multiple (16)
//        if (roundedValue < _min || roundedValue > _max)
//        {
//            roundedValue = RoundToNearest(_value, _fallbackMultiple);
//        }
//
//        // Ensure final rounded value is within the specified range
//        if (roundedValue < _min)
//            roundedValue = RoundToNearest(_min, _fallbackMultiple);
//        else if (roundedValue > _max)
//            roundedValue = RoundToNearest(_max, _fallbackMultiple);
//
//        return roundedValue;
//    }
//
//    static double RoundToNearest(double _value, int _multiple)
//    {
//        return Math.Round(_value / _multiple) * _multiple;
//    }
//}