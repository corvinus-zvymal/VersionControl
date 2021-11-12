using HetedikGyakorlat.Abstractions;
using HetedikGyakorlat.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HetedikGyakorlat
{
    public partial class Form1 : Form
    {
        List<Toy> _toys = new List<Toy>();
        Toy _nextToy;

        private IToyFactory _toyFactory;

        public IToyFactory ToyFactory
        {
            get { return _toyFactory; }
            set { _toyFactory = value; DisplayNext(); }
        }

        public Form1()
        {
            InitializeComponent();
            ToyFactory = new CarFactory();
        }

        private void createTimer_Tick(object sender, EventArgs e)
        {
            Toy ball = (Toy)ToyFactory.CreateNew();
            _toys.Add(ball);
            mainPanel.Controls.Add(ball);
            ball.Left = -ball.Width;
        }

        private void conveyorTimer_Tick(object sender, EventArgs e)
        {
            var lastPosition = 0;

            
            foreach (var item in _toys)
            {
                item.MoveToy();
                if (item.Left > lastPosition)
                {
                    lastPosition = item.Left;
                }
            }

            if (lastPosition>=1000)
            {
                var oldestToy = _toys[0];
                _toys.Remove(oldestToy);
                mainPanel.Controls.Remove(oldestToy);
            }
        }

        private void btnCar_Click(object sender, EventArgs e)
        {
            ToyFactory = new CarFactory();
        }

        private void btnBall_Click(object sender, EventArgs e)
        {
            ToyFactory = new BallFactory();
        }

        private void DisplayNext()
        {
            if (_nextToy != null)
            {
                this.Controls.Remove(_nextToy);
            }
            _nextToy = ToyFactory.CreateNew();
            _nextToy.Left = lblNext.Left + lblNext.Width;
            _nextToy.Top = lblNext.Top;
            this.Controls.Add(_nextToy);
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var cd = new ColorDialog();
            cd.Color = button.BackColor;
            if (cd.ShowDialog() != DialogResult.OK)
                return;
            button.BackColor = cd.Color;
        }
    }
}
