namespace LineAdjustment
{
    using System;
    using System.Text;

    public class LineAdjustmentAlgorithm
    {
        #region Constatns

        private const char STRING_DELIMETER = ' ';
        private const char PARAGRAPH_END = '\n';
        private const char STRING_SPACE = ' ';

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
            var strIndex = 0;

            while (strIndex < strArray.Length)
            {
                var startIndex = strIndex;
                var paragraphLength = 0;

                for (var i = startIndex; i < strArray.Length; i++)
                {
                    if (paragraphLength + strArray[strIndex].Length >= lineWidth)
                    {
                        break;
                    }

                    paragraphLength += strArray[strIndex].Length;
                    strIndex++;
                }

                var gaps = CalcGaps(strIndex - startIndex, paragraphLength, lineWidth);

                BuildParagraph(resultStr, strArray, startIndex, strIndex, gaps);
            }
                
            return resultStr.ToString();
        }

        private int[] CalcGaps(int wordCount, int paragraphLength, int lineWidth)
        {
            var spacesNumber = lineWidth - paragraphLength;
            var gapsNumber = wordCount - 1;

            if (gapsNumber < 1)
            {
                return new int[] {spacesNumber};
            }

            var gaps = new int[gapsNumber];
            var restSpaces = spacesNumber;

            for (int i = 0; i < gapsNumber; i++)
            {
                gaps[i] = spacesNumber / gapsNumber;
                restSpaces -= gaps[i];
            }

            while (restSpaces > 0)
            {
                for (int i = 0; i < gapsNumber; i++)
                {
                    gaps[i]++;
                    restSpaces--;
                    if (restSpaces < 1)
                    {
                        break;
                    }
                }
            }

            return gaps;
        }

        private void BuildParagraph(StringBuilder str, string[] data, int startIndex, int endIndex, int[] gaps)
        {
            var gapsIndex = 0;

            for (int i = startIndex; i < endIndex; i++)
            {
                str.Append(data[i]);

                if (gapsIndex >= gaps.Length)
                {
                    continue;
                }

                for (int j = 0; j < gaps[gapsIndex]; j++)
                {
                    str.Append(STRING_SPACE);
                }

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