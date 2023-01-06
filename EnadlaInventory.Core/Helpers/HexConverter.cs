namespace EnadlaInventory.Core.Helpers
{
    public static class HexConverter
    {
        private static readonly char[] HEX_CHARS = new char[] 
        { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F'};

        public static string ToHexSring(this IEnumerable<byte> bytes)
        {
            int bytesCount = bytes.Count();

            if (bytesCount <= 0)
            {
                return String.Empty;
            }

            char[] output = new char[bytesCount*2];

            int index = 0;
            foreach(byte b in bytes)
            {
                char[] hexForm = GetHexCharsFromByte(b);
                output[index] = hexForm[0];
                output[index+1] = hexForm[1];
                index += 2;
            }

            return new string(output);
        }

        private static char[] GetHexCharsFromByte(byte b)
        {
            return new char[] { HEX_CHARS[b >> 4], HEX_CHARS[b & 0b00001111u] };
        } 
    }
}
