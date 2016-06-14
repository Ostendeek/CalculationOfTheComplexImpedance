using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Numerics;

namespace CalculationOfTheComplexImpedance
{
    public partial class CalculateForm1 : Form
    {
        private List<double> _frequencies;
        private List<Complex> _result;
        private Circuit _myCircuit;

        private List<string> _resistorUnits = new List<string>();
        private List<string> _capacitorUnits = new List<string>();
        private List<string> _inductorUnits = new List<string>();


        private Dictionary<Types, List<string>> _elements = new Dictionary<Types, List<string>>();
//        {
//            {Types.Resistor, _resistorUnits},
//            {Types.Inductor, _inductorUnits},
//            {Types.Capacitor,_capacitorUnits}
//        };

        public CalculateForm1()
        {
            InitializeComponent();
            _myCircuit = new Circuit();
            _frequencies = new List<double>();

            foreach (Preambula preambula in Enum.GetValues(typeof(Preambula)))
            {
                _resistorUnits.Add(UnitTools.GetUnitsString(Types.Resistor, preambula));
                _capacitorUnits.Add(UnitTools.GetUnitsString(Types.Capacitor, preambula));
                _inductorUnits.Add(UnitTools.GetUnitsString(Types.Inductor, preambula));
            }

            _elements.Add(Types.Resistor, _resistorUnits);
            _elements.Add(Types.Inductor, _inductorUnits);
            _elements.Add(Types.Capacitor, _capacitorUnits);

            circuitDataGridView1.Columns.Add(new DataGridViewColumn(CreateCellConnection()));
            circuitDataGridView1.Columns[0].HeaderText = "Connection";
            circuitDataGridView1.Columns.Add(new DataGridViewColumn(CreateCellType()));
            circuitDataGridView1.Columns[1].HeaderText = "Element";
            circuitDataGridView1.Columns.Add(new DataGridViewColumn(new DataGridViewTextBoxCell()));
            circuitDataGridView1.Columns[2].HeaderText = "Nominal";
            circuitDataGridView1.Columns.Add(new DataGridViewColumn(CreateCellPreambula()));
            circuitDataGridView1.Columns[3].HeaderText = "Preambula";
           
        }

        public DataGridViewComboBoxCell CreateCellConnection()
        {
            DataGridViewComboBoxCell tmp = new DataGridViewComboBoxCell();
            tmp.Items.AddRange(Enum.GetNames(typeof(Connection)));
            return tmp;
        }
        public DataGridViewComboBoxCell CreateCellType()
        {
            DataGridViewComboBoxCell tmp = new DataGridViewComboBoxCell();
            tmp.Items.AddRange(Enum.GetNames(typeof(Types)));
            return tmp;
        }

        public DataGridViewComboBoxCell CreateCellPreambula()
        {
            DataGridViewComboBoxCell tmp = new DataGridViewComboBoxCell();
            tmp.Items.AddRange(Enum.GetNames(typeof(Preambula)));
         //   if ()
           // {
           //     circuitDataGridView1.Columns[2].Visible = true;
          //  }
            return tmp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void addButton_Click(object sender, EventArgs e)
        {
            //value = Convert.ToDouble(circuitDataGridView1[2, row].Value.ToString());
            _frequencies.Add(Convert.ToDouble(writeFrequenciesTextBox.Text));
            listBox1.Items.Add(writeFrequenciesTextBox.Text + " Hz");
            writeFrequenciesTextBox.Text = "";

        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            circuitDataGridView1.Rows.Clear();
        }

      

        private double GetValue(int row)
        {
            double value;
            value = Convert.ToDouble(circuitDataGridView1[2, row].Value.ToString());
            var p = UnitTools.GetPreambula(circuitDataGridView1[3, row].Value.ToString(),
                (Types) Enum.Parse(typeof(Types), circuitDataGridView1[1, row].Value.ToString()));
            sbyte step = (sbyte)p;
            value = value * (Math.Pow(10, step));
            return value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        /// 
        private string ComplexToString(Complex c)
        {
            string result = c.Real.ToString();
            if (c.Imaginary >= 0) result += "+";
            result += c.Imaginary.ToString();
            result += "j";

            return result;
        }
        
        private void circuitDataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1 && e.RowIndex >= 0)
            {
                var val = (string)circuitDataGridView1.Rows[e.RowIndex].Cells[1].Value;
                var type = (Types)Enum.Parse(typeof(Types), val);
                
                ((DataGridViewComboBoxCell)circuitDataGridView1.Rows[e.RowIndex].Cells[3]).Items.Clear();
                ((DataGridViewComboBoxCell)circuitDataGridView1.Rows[e.RowIndex].Cells[3]).Items
                    .AddRange(_elements[type].ToArray());
                ((DataGridViewComboBoxCell)circuitDataGridView1.Rows[e.RowIndex].Cells[3]).Value = _elements[type][0];
            }
           // if (e.Equals(Connection.Series))
            //{ }
        }

        private void calculateButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < circuitDataGridView1.RowCount - 1; i++)
            {
                string s = circuitDataGridView1[1, i].Value.ToString();
                string s1 = circuitDataGridView1[0, i].Value.ToString();
                var connection = (Connection)Enum.Parse(typeof(Connection), circuitDataGridView1[0, i].Value.ToString());
                try
                {
                    switch (s)
                    {
                        case "Capacitor": _myCircuit.Elements.Add(new Capacitor(
                            GetValue(i), connection));
                            break;
                        case "Inductor": _myCircuit.Elements.Add(new Inductor(
                            GetValue(i), connection));
                            break;
                        case "Resistor": _myCircuit.Elements.Add(new Resistor(
                            GetValue(i), connection));
                            break;
                        default: 
                            MessageBox.Show("write the correct value", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                    }
                }
                catch 
                { 
                    MessageBox.Show("write the correct value"); 
                }
            }
            _myCircuit.Frequencies = _frequencies;
            _result = _myCircuit.CalculateZ();
            listBox1.Items.Clear();

            for (int i = 0; i < _result.Count; i++)
            {
                listBox1.Items.Add(_frequencies[i] + " Hz - " + ComplexToString(_result[i]) + " Ohm");
            }

        }

        private void writeFrequenciesTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
            /*if ((e.KeyChar <= '0' || e.KeyChar >= '9') && e.KeyChar != 8)
            {
                char.
                //e.KeyChar == e.Equals != 0;
                e.Handled = true;
            } */
        }

      
    }
}
