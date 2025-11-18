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

            isFilterMenuOpen = false;
            haveSqlMode = false;
            haveTransferMode = false;

            comboBoxArrayList.Visible = false;
            textBoxArrayA.Visible = false;
            textBoxArrayB.Visible = false;
            buttonSave.Enabled = false;
            groupBoxArrayList.Visible = false;
            groupBoxFilter.Visible = false;
            groupBoxTransfer.Visible = false;


            /*
             * groupBoxFilter.Controls.Add(checkBoxNotSorted);
            groupBoxFilter.Controls.Add(checkBoxSorted);

            groupBoxTransfer.Controls.Add(buttonA);
            groupBoxTransfer.Controls.Add(buttonB);

            buttonTransfer.Controls.Add(groupBoxTransfer);
            buttonFilter.Controls.Add(groupBoxFilter);

            groupBoxArrayList.Controls.Add(buttonTransfer);
            groupBoxArrayList.Controls.Add(buttonFilter);
            groupBoxArrayList.Controls.Add(comboBoxArrayList);

            buttonSqlMode.Controls.Add(groupBoxArrayList);
            buttonSqlMode.Controls.Add(textBoxArrayA);
            buttonSqlMode.Controls.Add(textBoxArrayB);
            */
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {

        }

        private void buttonSorting_Click(object sender, EventArgs e)
        {

        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {

        }

        private void textBoxArrayA_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonSqlMode_Click(object sender, EventArgs e)
        {
            if (!haveSqlMode)
            {
                comboBoxArrayList.Visible = true;
                textBoxArrayA.Visible = true;
                textBoxArrayB.Visible = true;
                buttonSave.Enabled = true;
                groupBoxArrayList.Visible = true;
                haveSqlMode = true;
                return;
            }

            comboBoxArrayList.Visible = false;
            textBoxArrayA.Visible = false;
            textBoxArrayB.Visible = false;
            buttonSave.Enabled = false;
            groupBoxArrayList.Visible = false;
            haveSqlMode = false;
        }

        private void buttonRandom_Click(object sender, EventArgs e)
        {

        }

        private void hideControl(Control obj, out bool trigger)
        {
            obj.Visible = false;
            trigger = false;
        }

        private void hideControls(Control[] objs, out bool trigger)
        {
            foreach (var obj in objs)
            {
                obj.Visible = false;
            }
            trigger = false;
        }

        private void showControl(Control obj, out bool trigger)
        {
            obj.Visible = true;
            trigger = true;
        }

        private void showControls(Control[] objs, out bool trigger)
        {
            foreach (var obj in objs)
            {
                obj.Visible = true;
            }
            trigger = true;
        }

        private void buttonTransfer_Click(object sender, EventArgs e)
        {
            if (!haveTransferMode)
            {
                showControl(groupBoxTransfer, out haveTransferMode);
                hideControl(groupBoxFilter, out isFilterMenuOpen);
                return;
            }
            hideControl(groupBoxTransfer, out haveTransferMode);
        }

        private void buttonFilter_Click(object sender, EventArgs e)
        {
            if (!isFilterMenuOpen)
            {
                showControl(groupBoxFilter, out isFilterMenuOpen);
                hideControl(groupBoxTransfer, out haveTransferMode);
                return;
            }
            hideControl(groupBoxFilter, out isFilterMenuOpen);
        }
    }
}
