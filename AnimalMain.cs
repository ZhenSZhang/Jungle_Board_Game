using Mines;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Animal
{

    public partial class AnimalMain : Form
    {
        /// <summary>
        /// timer: initialize timer at 0
        /// </summary>
        private int timeCount = 0;

        /// <summary>
        /// rows and colums
        /// </summary>
        private int wNum, hNum;
        /// <summary>
        /// size of each block
        /// </summary>
        private int edge = 80;

        /// <summary>
        /// game status
        /// </summary>
        private string status = "stop";
        /// <summary>
        /// current color of player(red or blue)
        /// </summary>
        private int this_user;
        /// <summary>
        /// current button (selected animal)
        /// </summary>
        private Button selectButton;
        private string user1_color;
        private string user2_color;
        //list of animals remained of both colors
        List<Button> Red_buttons = new List<Button>();
        List<Button> Blue_buttons = new List<Button>();


        public AnimalMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// creates all buttons
        /// </summary>
        /// <param name="animal_type"></param>
        /// <param name="count"></param>
        /// <param name="use_type"></param>
        private List<Button> initAnimals(int animal_type, int count, string use_type)
        {
            //save all buttons initially created
            List<Button> btns = new List<Button>();
            for (int i = 0; i < count; i++)
            {
                //creates button
                Button btn = new Button();
                btn.Height = edge;
                btn.Width = edge;
                btn.BackColor = Color.Lavender;
                //initialize animals
                animal a = new animal(animal_type, use_type);
                //link animal to button
                btn.Tag = a;
            
                btns.Add(btn);
            }
            return btns;
        }

        /// <summary>
        /// Create buttons of both color
        /// </summary>
        private void createBtns()
        {
            //create red buttons
            List<Button> animals_config = new List<Button>();
            Red_buttons.AddRange(initAnimals(AnimalType.mouse, 2, "red"));
            Red_buttons.AddRange(initAnimals(AnimalType.cat, 2, "red"));
            Red_buttons.AddRange(initAnimals(AnimalType.dog, 2, "red"));
            Red_buttons.AddRange(initAnimals(AnimalType.wolf, 2, "red"));
            Red_buttons.AddRange(initAnimals(AnimalType.panther, 2, "red"));
            Red_buttons.AddRange(initAnimals(AnimalType.tiger, 2, "red"));
            Red_buttons.AddRange(initAnimals(AnimalType.lion, 2, "red"));
            Red_buttons.AddRange(initAnimals(AnimalType.elephant, 2, "red"));

            //create blue buttons
            Blue_buttons.AddRange(initAnimals(AnimalType.mouse, 2, "blue"));
            Blue_buttons.AddRange(initAnimals(AnimalType.cat, 2, "blue"));
            Blue_buttons.AddRange(initAnimals(AnimalType.dog, 2, "blue"));
            Blue_buttons.AddRange(initAnimals(AnimalType.wolf, 2, "blue"));
            Blue_buttons.AddRange(initAnimals(AnimalType.panther, 2, "blue"));
            Blue_buttons.AddRange(initAnimals(AnimalType.tiger, 2, "blue"));
            Blue_buttons.AddRange(initAnimals(AnimalType.lion, 2, "blue"));
            Blue_buttons.AddRange(initAnimals(AnimalType.elephant, 2, "blue"));
            //put them all into list of ramdomly-selecting-list
            animals_config.AddRange(Blue_buttons);
            animals_config.AddRange(Red_buttons);
            Random ra = new Random();
            //control button position
            int left = 0;
            int top = 0;
            int i = 0;
            //set window size
            int width = edge * wNum + 18;
            int height = edge * (hNum + 1) + 26 + 35;
            this.Width = width;
            this.Height = height;
            //ramdonly assign buttons across the board
            while (animals_config.Count > 0)
            {
                //ramdonly get a button
                int radom_index = ra.Next(0, animals_config.Count - 1);
                Button btn = animals_config[radom_index];
                
                //create clicking event
                btn.Click += animalClick;
                //creates a panel
                Panel p = new Panel();
                p.BorderStyle = BorderStyle.FixedSingle;
                p.Controls.Add(btn);
                //panel position
                p.Location = new Point(left, top);
                p.Height = edge;
                p.Width = edge;
                p.Click += moveClick;
                //add to panel
                this.panel2.Controls.Add(p);
                //remove button from list
                animals_config.RemoveAt(radom_index);
                left += this.edge;
                //add one more button count
                i++;
                //change row after placed 8 buttons 
                if (i == 8)
                {
                    i = 0;
                    left = 0;
                    top += this.edge;
                }
            }


        }
        /// <summary>
        /// left click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void moveClick(object sender, EventArgs e)
        {
            if (this.selectButton == null)
                return;
            //if null, return
            if ((sender as Panel).Controls.Count > 0)
                return;
            //can only move up down left or right
            Point p1 = selectButton.Parent.Location;//selct
            Point p2 = (sender as Panel).Location;//target
            if (p1.X != p2.X && p1.Y != p2.Y)
            {
                return;
            }
            if (p1.Y == p2.Y)
            {
                if (Math.Abs(p1.X - p2.X) > this.edge)
                    return;
            }
            else
            {
                if (Math.Abs(p1.Y - p2.Y) > this.edge)
                    return;
            }


            selectButton.Parent.Controls.Remove(selectButton);
            (sender as Panel).Controls.Add(selectButton);
            this.selectButton = null;
            bool ove = isOver();
            if (ove)
            {
                MessageBox.Show("Congrat! You Won!");
                status = "over";
                return;
            }
            //swtich color player
            this.this_user = this_user == 1 ? 2 : 1;
            displayUser();
        }

        /// <summary>
        /// left click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void animalClick(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            // achieve animal
            animal a = btn.Tag as animal;
            //if status is not start, then return
            if (status != "start")
                return;
            string color = this_user == 1 ? user1_color : user2_color;
            if (!a.isUse)
            {
                //Version 1: display text
              //  btn.Text = AnimalType.getText(a.animal_type);
                //set blank background is white
                btn.BackColor = Color.White;
              
                if (a.user_type == "blue")
                {
                    //version 1: change text color
                    //btn.ForeColor = Color.Blue;
                    //version 2:add picture
                    btn.BackgroundImage = Image.FromFile(AnimalType.getImageBlue(a.animal_type));
                    btn.BackgroundImageLayout = ImageLayout.Stretch;
                }
                else
                {
                    //version 1: change text color
                    //btn.ForeColor = Color.Red;
                    //version 2:add picture
                    btn.BackgroundImage = Image.FromFile(AnimalType.getImageRed(a.animal_type));
                    btn.BackgroundImageLayout = ImageLayout.Stretch;
                }
                //change status to uncovered
                a.isUse = true;       //initial click to determine which color is next
                if (user1_color == null)
                {
                    user1_color = a.user_type;
                    if (user1_color == "blue")
                    {
                        user2_color = "red";
                        //switch color
                        this.this_user = 2;
                        displayUser();
                    }
                    else
                    {
                        user2_color = "blue";
                        //switch color
                        this.this_user = 2;
                        displayUser();

                    }
                }
                else
                {
                    //switch
                    this.this_user = this_user == 1 ? 2 : 1;
                    displayUser();
                }
                selectButton = null;
            }
            else if (color == a.user_type)
            {
                //select button
                this.selectButton = btn;
            }
            else if (selectButton != null)
            {
                //attack

                //determine distance
                Point p1 = (btn.Parent as Panel).Location;
                Point p2 = (selectButton.Parent as Panel).Location;
                //can only move up down left or right
                if (p1.X != p2.X && p1.Y != p2.Y)
                {
                    return;
                }
                if (p1.Y == p2.Y)
                {
                    if (Math.Abs(p1.X - p2.X) > this.edge)
                        return;
                }
                else
                {
                    if (Math.Abs(p1.Y - p2.Y) > this.edge)
                        return;
                }
                //compare
                animal a2 = this.selectButton.Tag as animal;
                //cannot beat your own animals
                if (a2.user_type == a.user_type)
                    return;
                if (!AnimalType.canEat(a2.animal_type, a.animal_type))
                {
                    //cannot beat
                    return;
                }
                //can beat
                selectButton.Controls.Remove(selectButton);
                btn.Parent.Controls.Add(selectButton);
                btn.Parent.Controls.Remove(btn);
                //remove beaten ones
                if (a.user_type == "blue")
                {
                    Blue_buttons.Remove(btn);
                }
                else
                {
                    Red_buttons.Remove(btn);
                }
                bool ove = isOver();
                
                if (ove)
                {
                    MessageBox.Show("Congrats! You Won!");
                    status = "over";
                    return;
                }
                else
                {
                    //switch player
                    this.this_user = this_user == 1 ? 2 : 1;
                    displayUser();
                }
                this.selectButton = null;
            }
        }
        private bool isOver()
        {
            //see if game is over
            List<Button> btns;
            string color = this_user == 1 ? user1_color : user2_color;
            if (color == "blue")
            {
                btns = Red_buttons;
            }
            else
            {
                btns = Blue_buttons;
            }
            //if all were beaten
            if (btns.Count == 0)
            {
                return true;
            }
            return false;

        }

        private void displayUser()
        {
            string color = this_user == 1 ? user1_color : user2_color;
            if (color == "blue")
            {
                //Choose which side's turn
                label1.Text = "Blue's Turn";
            }
            else
            {
                label1.Text = "Red's Turn";
            }


        }
        /// <summary>
        /// start new game
        /// </summary>
        private void startGame()
        {
            wNum = 8;
            hNum = 4;
            timeCount = 0;
            lbl_time.Text = timeCount.ToString();
            //clean chess board
            this.panel2.Controls.Clear();
          
            //reset user's information
            this_user = 1;
            user1_color = null;
            user2_color = null;
            this.Blue_buttons.Clear();
            this.Red_buttons.Clear();
            selectButton = null;
            status = "start";
            //re-start
            createBtns();
        }

        /// <summary>
        /// new game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            startGame();
            setStart();
        }
     
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        /// <summary>
        /// timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_Tick(object sender, EventArgs e)
        {
            timeCount++;
            this.lbl_time.Text = timeCount.ToString();
        }

        private void RulesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GUIZE g = new GUIZE();
            g.ShowDialog();
        }

        /// <summary>
        ///start timers after first click
        /// </summary>
        public void setStart()
        {
            this.timer.Enabled = true;
        }
    }
    public class animal
    {
        public animal(int animal_type, string user_type)
        {
            this.animal_type = animal_type;
            this.user_type = user_type;
            this.isUse = false;
        }
        public int animal_type { set; get; }
        public string user_type { set; get; }
        public bool isUse { set; get; }
    }
}
