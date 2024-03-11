namespace Wspolbiegi0
{
    public class Addition
    {
        public Addition()
        {
        }

        public int Add(int[] array)
        {
            int resault = 0;
            for (int i = 0; i < array.Length; i++)
            {
                resault += array[i];
            }
            return resault;
        }
    }

}
