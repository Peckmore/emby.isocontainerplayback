namespace IsoContainerPlayback.TestClient
{
    public partial class PickerForm : Form
    {
        #region Construction

        public PickerForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Properties

        public string? SelectedItem { get; private set; }

        #endregion

        #region Methods

        #region Event Handlers

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        private void buttonOpen_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
        private void listBox_DoubleClick(object sender, EventArgs e)
        {
            DialogResult = SelectedItem!= null ? DialogResult.OK : DialogResult.Cancel;
            Close();
        }
        private void listBox_SelectedValueChanged(object sender, EventArgs e)
        {
            SelectedItem = listBox.SelectedItem?.ToString();
        }

        #endregion

        #region Public

        public void AddItem(string item)
        {
            listBox.Items.Add(item);
        }

        #endregion

        #endregion
    }
}