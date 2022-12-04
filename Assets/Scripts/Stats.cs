using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public static class Stats
    {
        public static int HurdLevel { get; private set; } = 1;
        private static int _score = 0;

        public static int Score
        {
            get
            {
                return _score;
            }
            set
            {
                _score = value;
                if(_score > 100 * HurdLevel) 
                {
                    HurdLevel++;
                    _score = 0;
                }
            }

        }

        public static void ResetAllStats()
        {
            HurdLevel = 1;
            _score = 0;
        }

    }
