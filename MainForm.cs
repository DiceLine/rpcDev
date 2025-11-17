namespace WinFormsRPC
{
    public partial class MainForm : Form
    {
        private static void Swap(int[] array, int i, int j)
        {
            int temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }

        private static int Partition(int[] array, int left, int right)
        {
            int pivot = array[(left + right) / 2];

            while (left <= right)
            {
                while (array[left] < pivot) ++left;
                while (array[right] > pivot) --right;
                if (left <= right)
                {
                    Swap(array, left, right);
                    ++left;
                    --right;
                }
            }

            return left;
        }

        private static void QuickSort(int[] array, int left, int right)
        {
            if (left >= right) return;
            int pivotIndex = Partition(array, left, right);
            QuickSort(array, left, pivotIndex - 1);
            QuickSort(array, pivotIndex, right);
        }

        public static void QuickSort(int[] array)
        {
            QuickSort(array, 0, array.Length - 1);
        }

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}