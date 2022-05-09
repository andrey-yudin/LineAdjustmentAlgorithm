namespace LineAdjustment
{
    using System;
    using System.Text;

    public class LineAdjustmentAlgorithm
    {
        #region Constatns

        private const char STRING_DELIMETER = ' ';
        private const char PARAGRAPH_END = '\n';
        private const string STRING_SPACE = " ";

        #endregion Constants

        #region Methods

        public string Transform(string input, int lineWidth)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }

            var resultStr = new StringBuilder(input.Length);

            var strArray = input.Split(STRING_DELIMETER, StringSplitOptions.RemoveEmptyEntries);
            int strIndex = 0;
            int startIndex = strIndex;
            int paragraphLength = 0;

            while (strIndex < strArray.Length)
            {
                if (paragraphLength + strArray[strIndex].Length < lineWidth)
                {
                    paragraphLength += strArray[strIndex].Length;
                    strIndex++;

                    if (strIndex < strArray.Length)
                    {
                        continue;
                    }
                }

                var gaps = CalcGaps(strIndex - startIndex, lineWidth - paragraphLength);

                BuildParagraph(resultStr, strArray, startIndex, strIndex, gaps);

                startIndex = strIndex;
                paragraphLength = 0;
            }
                
            return resultStr.ToString();
        }

        private int[] CalcGaps(int wordCount, int spacesNumber)
        {
            int gapsNumber = wordCount > 1 ? wordCount - 1 : wordCount;

            var gaps = new int[gapsNumber];

            for (int i = 0; i < gapsNumber; i++)
            {
                gaps[i] = spacesNumber / gapsNumber;
            }

            int restSpaces = spacesNumber - (spacesNumber / gapsNumber) * gapsNumber;

            int gapsIndex = 0;

            while (restSpaces > 0)
            {
                gaps[gapsIndex++]++;
                restSpaces--;

                if (gapsIndex >= gapsNumber)
                {
                    gapsIndex = 0;
                }
            }

            return gaps;
        }

        private void BuildParagraph(StringBuilder str, string[] data, int startIndex, int endIndex, int[] gaps)
        {
            int gapsIndex = 0;

            for (int i = startIndex; i < endIndex; i++)
            {
                str.Append(data[i]);

                if (gapsIndex >= gaps.Length)
                {
                    continue;
                }

                str.Insert(str.Length, STRING_SPACE, gaps[gapsIndex]);

                gapsIndex++;
            }

            if (data.Length > 1 && endIndex < data.Length)
            {
                str.Append(PARAGRAPH_END);
            }
        }

        #endregion Methods
    }
}