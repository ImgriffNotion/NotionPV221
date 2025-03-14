namespace NotionBack.Services.RandomService
{
    public class RandomCreatorService : IRandomService
    {
        public static Random _random = new Random();

        public String CreatorOnePassCodeByRandom()
        {
            int countOfSymbols = 8;
            String onePassCode = new String("");

            for (int i = 0; i < countOfSymbols; i++)
            {
                int tmpSymbCode = _random.Next(0, 9);
                if ((tmpSymbCode & 1) == 1)
                {
                    onePassCode += Convert.ToChar(_random.Next(97, 122));
                }
                else
                {
                    onePassCode += Convert.ToChar(_random.Next(48, 57));
                }
            }

            return onePassCode;
        }

    }
}
