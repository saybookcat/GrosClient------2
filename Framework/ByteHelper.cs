namespace Framework
{
    public class ByteHelper
    {
        public static byte GetHighByte(short value)
        {
            return (byte)((value & 0xf0) >> 4);
        }

        public static byte GetHighByte(sbyte value)
        {
            return (byte)((value & 0xf0) >> 4);
        }

        public static byte GetLowByte(short value)
        {
            return (byte)(value & 0xf);
        }

        public static byte GetLowByte(sbyte value)
        {
            return (byte)(value & 0xf);
        }
    }
}
