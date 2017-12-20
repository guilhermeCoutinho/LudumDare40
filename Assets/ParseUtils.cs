using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets
{
    public class ParseUtils
    {
        public static List<string> TextAssetToList(TextAsset ta)
        {

            var listToReturn = new List<string>();

            var arrayString = ta.text.Split('\n');

            foreach (var line in arrayString)
            {
                listToReturn.Add(line);
            }

            return listToReturn;

        }

    }
}
