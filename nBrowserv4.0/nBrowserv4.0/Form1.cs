using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Speech.Synthesis;
using System.Text.RegularExpressions;

namespace nBrowserv4._0
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Define all variables
        public string learning_objective = "Your learning objective is to revise the design of activities described in the lesson with the aim of using technology to promote teaching and learning practices.";
        public string task = "";
        public int tasktime;
        public int timer1mincount;
        public int countcheckeditems;
        public string activitytype = "";
        public string activitylabel = "";
        public string activitydescription = "";
        public string activitysubject = "";
        public string activitylevel = "";
        public string activitystandard = "";
        public string activityurl = "";
        public string techname = "";
        public string techsource = "";
        public string techaffordance = "";
        public string techjustification = "";
        public string criteria1 = "";
        public string criteria2 = "";
        public string criteria3 = "";
        public string criteria4 = "";
        public string criteria5 = "";
        public string criteria6 = "";
        public string criteria7 = "";
        public string criteria8 = "";
        public string remindprompt1 = "false";
        public string remindprompt2 = "false";
        public string remindprompt3 = "false";
        public string remindprompt4 = "false";
        public int timestampmillisec;


        // Amy dialog moves
        public string amy_setsubgoal = "Choose a task from the dropdown menu and decide how much time should be allocated to this task. Select Review to submit your edits to the lesson.";
        public string amy_reviewsubgoal1 = "Please choose a task from the dropdown menu.";
        public string amy_reviewsubgoal2 = "Please allocate a certain amount of time to spend on this task.";
        public string amy_remindsubgoal = "Don't forget to set a task from the dropdown menu to choose a task and allocate a certain amount of time to complete it.";

        public string amy_informationseeking1 = "I've already selected a lesson for you to review, you can select the home icon to navigate back to this lesson at any time."; //scaffolds selecting a new informational source
        public string amy_informationseeking2 = "Begin by quickly reading through the lesson to preview the structure of the content."; //scaffolds looking for specific information; varying reading rate +
        public string amy_informationseeking3 = "Now, read it again carefully to look for information that pertains to your goal. Where in the lesson do you expect to find this information? How will this information help you reach your goal? Write the relevant information to describe the activity in the solution explorer panel.  Select Review to submit your edits to the lesson."; //scaffolds varying reading rate -; expectation of adequacy of content; highlight, skip, or reinstate information
        public string amy_informationseeking4 = "Do you feel like you are comfortable with the content/subject matter? If not, you may want to spend some time to search for relevant information from external sources on the web."; // Feeling of knowing/Judgement of knowing (while searching)

        public string amy_reviewinformation1 = "Please indicate the type of activity that you've found.";
        public string amy_reviewinformation2 = "Please write a brief label for the activity.";
        public string amy_reviewinformation3 = "Please describe further activity by copying and pasting it from the lesson plan.";
        public string amy_reviewinformation4 = "Please identify the relevant subject matter taught during the activity.";
        public string amy_reviewinformation5 = "Please identify the relevant grade level appropriate for this activity.";
        public string amy_reviewinformation6 = "Please identify the relevant curricular standard targeted by the activity.";
        public string amy_reviewinformation7 = "Please write the url address to the lesson plan that describes the activity.";

        public string amy_remindinformationseeking = "Don't forget to describe the activity from the lesson, you should move forward to elaborating further how to use technology in the next step.";

        public string amy_taskconstruction1 = "Let’s now elaborate further on the information taken from the lesson. How would you use technology as a means to achieve your goal? Search online resources if necessary or draw from your own experiences using technologies in the classroom.  Select Review to submit your edits to the lesson."; // Elaborating information, control of context, interest
        public string amy_taskconstruction2 = "Do you feel like the affordances of technology are aligned with your goal? How do these affordances support learning and teaching practices?"; // Monitor the use of strategies

        public string amy_reviewtaskconstruction1 = "Please write a name for the technology used in the activity.";
        public string amy_reviewtaskconstruction2 = "Please write the url address to the technology used in the activity.";
        public string amy_reviewtaskconstruction3 = "Please identify the affordance of the technology in promoting teaching and learning activities.";
        public string amy_reviewtaskconstruction4 = "Please elaborate further on your justification for how this technology provides the affordances stated for your activity.";

        public string amy_remindtaskconstruction = "Please complete this section on the use of technology, we will move forward to working on assessing the activity in the next step.";

        public string amy_solutionassessment1 = "Do you feel like your revised lesson activity achieves your stated goal?"; // Monitor progress towards goals
        public string amy_solutionassessment2 = "Are you feeling any interest in trying this activity for your own once you will be teaching in a classroom on the same topic/grade level?"; // Monitor emotional reactions
        public string amy_solutionassessment3 = "Do you have any doubts on the feasibility or clarity of the activity, as described in the lesson?"; // Self-Questioning
        public string amy_solutionassessment4 = "Do you feel like your revised lesson activity is now well suited towards solving the issue described in the teacher case?"; // Connecting Information with Current/Past Practice

        public string amy_reviewassessment1 = "Please select a rating for each criteria to evaluate the activity. Select Review to submit your edits to the lesson.";

        public string amy_remindsolutionassessment = "Please indicate a rating for each criteria for your activity.";

        public string amy_independentpractice = "Finish your lesson by making last revisions to the design. Select the dashboard tab to review the details of the lesson. Let the experimenter know when you are ready to submit your lesson plan.";

        public string amy_recommendation = "I would recommend to describe the following activity. I've navigated to the relevant location in the lesson plan, please take a few moments to read the details.";

        public string amy_peercomparison = "Select the compare activities icon in the solution view panel to view an example lesson to compare your activity with an example lesson where technology is used to promote teaching and learning.";

        // List of Conversational States
        public string dialog_request = "";
        public string interface_status = "";
        public string fadedscaffold = "false";

        // List of hint variables
        public string hintlevel = "level1";
        public string hintcontent = "";

        // Example Lesson Plan Delivered from Peer Review
        public string examplepeerlesson = "";

        // Log File variables
        public int Logfileentrycount;
        public int Logfile2entrycount;

        // Initialize speech synthesis
        SpeechSynthesizer synth = new SpeechSynthesizer();

        // Network-Based Domain Model
        string[] elements = {
            "T: Let’s add fractions mentally.  Say answers as whole numbers when possible.",
            "T: Write 2/4. Rename the fraction by writing the largest unit possible.",
            "One ninth of the students in Mr. Beck’s class list red as their favorite color.  Twice as many students call blue their favorite, and three times as many students prefer pink.  The rest name green as their favorite color.  ",
            "T: (Write 1 adult + 3 adults.)",
            "T: (Write 1 child + 3 adults.)",
            "T: (Write 1/2 + 1/4.)",
            "In this problem, students can fold a paper again to transition into drawing, or start directly with drawing.  This is a simple problem involving two unit fractions, such as Problem 1.  The primary purpose is to reinforce understanding of what is occurring to the units within a very simple context.  Problem 3 moves forward to address a unit fraction plus a non-unit fraction.",
            "T: When we partition a rectangle into thirds,",
            "T: Let’s make these units the same size.",
            "T: (Write 2/7 + 2/3.) Work with your partner to solve this problem.",
            "T: For one minute, go over the answers to Problem 1 with your partner.  Don’t change your work.",
            "T: With your partner, review the process we used to solve 2/3 + 1/4 step by step. Partner A goes first, and then partner B. Draw an area model to show how you make equivalent fractions to add unlike units",
            "Note:  This problem adds the complexity of finding the sum of two non-unit fractions, both with the numerator of 2.  Working with fractions with common numerators invites healthy reflection on the size of fifths as compared to thirds.  Students can reason that, while there are the same number of units (2), thirds are larger than fifths because the whole is broken into 3 parts instead of 5 parts.  Therefore, there are more in each part.  Additionally, it can be reasoned that 2 thirds is larger than 2 fifths because when fifteenths are used for both, the number of units in 2 thirds (10) is more than the number used in 2 fifths (6).",
            "Lesson Objective:  Add fractions with unlike units using the strategy of creating equivalent fractions.<br>The Student Debrief is intended to invite reflection and active processing of the total lesson experience.<br>Invite students to review their solutions for the Problem Set.  They should check work by comparing answers with a partner before going over answers as a class. Look for misconceptions or misunderstandings that can be addressed in the Debrief.  Guide students in a conversation to debrief the Problem Set and process the lesson.",
            "<p>T: I am glad to hear you are able to point out relationships between different problems.</p><p>T: Share with your partner what you learned how to do today.</p><p>S: (Share.)</p><p>T: (Help students name the objective:  We learned how to add fractions that have unlike units using a rectangular fraction model to create like units.)</p>",
            "After the Student Debrief, instruct students to complete the Exit Ticket.  A review of their work will help you assess the students’ understanding of the concepts that were presented in the lesson today and plan more effectively for future lessons.  You may read the questions aloud to the students.",
            "<p>T: 1/3 + 1/3 = ?</p><p>S: 2/3.</p><p>T: 1/4 + 1/4 = ?</p><p>S: 2/4.</p><p>T: 1/5 + 2/5 = ?</p><p>S: 3/5.</p><p>T: 3/7 + 4/7 = ?</p><p>S: 1.</p><p>T: 1/4 + 1/3 + 3/4 + 2/3 = ?</p><p>S: 2.</p><p>Continue and adjust to meet student needs.  Use a variety of fraction combinations.</p>",
            "<p>S: Write 1/2.</p><p>T: Write 3/6. Try this problem.</p><p>S: Write 1/2.</p><p>Continue with the following possible sequence: 6/12, 3/9, 2/6, 4/6, 6/9, 2/8, 3/12, 4/16, 12/16, 9/12, and 6/8.</p>",
            "What is 1 adult plus 3 adults?</p><p>S: 4 adults.</p><p>T: 1 fifth plus 3 fifths?</p><p>S: 4 fifths.</p><p>T: We can add 1 fifth plus 3 fifths because the units are the same. <br>1 fifth + 3 fifths = 4 fifths. <br>1/5 + 3/5 = 4/5</p>",
            "What is 1 child plus 3 adults?</p><p>S: We can’t add children and adults.</p>",
            "How many units will I have if I partition 1 whole into smaller units of one half each?</p><p>S: 2 units.</p><p>T: (Partition the rectangle vertically into 2 equal units.)  One half tells me to select how many of the 2 units?</p><p>S: One.</p><p>T: Let’s label our unit with 1/2 and shade in one part.  Now, let’s draw another whole rectangle.  How many equal parts do I divide this whole into to make fourths?</p><p>S: Four.</p><p>T: (Partition the rectangle horizontally into 4 equal units.)  One fourth tells me to shade how many units?</p><p>S: One.</p><p>T: Let’s label our unit with 1/4 and shade in one part.  Now, let’s partition our 2 wholes into the same size units. (Draw horizontal lines on the 1/2 model and 1 vertical line on the 1/4 model.)  What fractional unit have we made for each whole?</p><p>S: Eighths.</p><p>T: How many shaded units are in 1/2?</p><p>S: Four</p><p>T: That’s right; we have 4 shaded units out of 8 total units. (Change the label from 1/2 to 4/8.)  How many units are shaded on the 1/4 model?</p><p>S: Two.</p><p>T: Yes, 2 shaded parts out of 8 total parts.  (Change the label from 1/4 to 2/8 .)  Do our models show like units now?</p><p>S: Yes!</p><p>T: Say the addition sentence now using eighths as our common denominator, or common unit.</p><p>S: 4 eighths + 2 eighths = 6 eighths.</p>",
            "<p>T:  Do our units get larger or smaller when we create like units?  Talk to your partner.</p><p>S: The units get smaller.  There are more units, and they are definitely getting smaller. The units get smaller. It is the same amount of space, but more parts. We have to cut them up to make them the same size. We can also think how 1 unit will become 6 units.  That’s what is happening to the half.</p>",
            "<p>T: Did the half become 3 smaller units and each third become 2 smaller units?</p><p>S: Yes!</p><p>T: Tell me the addition sentence.</p><p>S: 2 sixths + 3 sixths = 5 sixths.<br>1/3 + 1/2 = 2/6 + 3/6 = 5/6.</p>",
            "how many units do we have in all?</p><p>S: 3.</p><p>T: (Partition thirds vertically.)  How many of those units are we shading?</p><p>S: 2.</p><p>T: (Shade and label 2 thirds.)  To show 1 fourth, how many units do we draw?</p><p>S: 4.</p><p>T: (Make a new rectangle of the same size and partition fourths horizontally.)</p><p>T: How many total units does this new rectangle have?</p><p>S: 4.</p><p>T: Shade and label the new rectangle.)</p>",
            "(Partition the rectangles so the units are equal.)</p><p>T: What is the fractional value of 1 unit?</p><p>S: 1 twelfth</p><p>T: How many twelfths are equal to 2 thirds?</p><p>S: 8 twelfths.</p><p>T: (Mark 8/12 on the 2/3 rectangle.) How many twelfths are equal to 1/4?</p><p>S: 3 twelfths.</p><p>T: (Mark 3/12 on the 1/4 rectangle.) Say the addition sentence now using twelfths as our like unit or denominator.</p><p>S: 8 twelfths plus 3 twelfths equals 11 twelfths.<br>2/3 + 1/4 = 8/12 + 3/12 = 11/12</p><p>T: Read with me.  2 thirds + 1 fourth = 8 twelfths + 3 twelfths = 11 twelfths.</p>",
            "<p>S: (Work.) 2 sevenths + 2 thirds = 6 twenty-firsts + 14 twentyfirsts = 20 twenty-firsts.<br>2/7 + 2/3 = 6/21 + 14/21 = 20/21.</p>",
            "Students should do their personal best to complete the Problem Set within the allotted 10 minutes.  For some classes, it may be appropriate to modify the assignment by specifying which problems they work on first.  Some problems do not specify a method for solving.  Students should solve these problems using the RDW approach used for Application Problems.",
            "<p>S: (Work together.)</p><p>T: Now, let’s correct errors together.  I will say the addition problem; you will say the answer.Problem 1(a).  1 half plus 1 third is…?</p><p>S: 5 sixths.</p><p><em>Continue with Problems 1(b–f).  Then, give students about 2 minutes to correct their errors.</em></p><p>T: Analyze the following problems.How are they related? <br>Problems 1 (a) and (b)<br>Problems 1 (a) and (c)<br>Problems 1 (b) and (d)<br>Problems 1 (d) and (f)</p><p>S: (Discuss.)</p><p>T: Steven noticed something about Problems 1 (a) and (b). Please share.</p><p>S: The answer to (b) is smaller than (a) since you are adding only 1/5 to 1/2. Both answers are less than 1, but (a) is much closer to 1. Problem (b) is really close to 1/2 because 8/16 would be 1/2.</p><p>T: Kara, can you share what you noticed about Problems 1(d) and (f)?</p><p>S: I noticed that both problems used thirds and sevenths.But the numerators in (d) were 1, and the numerators in (f) were 2.  Since the numerators doubled, the answer doubled from 10 twenty-firsts to 20 twenty-firsts.</p>"
        };

        string[] urls =
        {
            "https://s3.amazonaws.com/novessa/lessons/lessong5sA1n1Printer.html#id1",
            "https://s3.amazonaws.com/novessa/lessons/lessong5sA1n1Printer.html#id2",
            "https://s3.amazonaws.com/novessa/lessons/lessong5sA1n1Printer.html#id3",
            "https://s3.amazonaws.com/novessa/lessons/lessong5sA1n1Printer.html#id4",
            "https://s3.amazonaws.com/novessa/lessons/lessong5sA1n1Printer.html#id5",
            "https://s3.amazonaws.com/novessa/lessons/lessong5sA1n1Printer.html#id6",
            "https://s3.amazonaws.com/novessa/lessons/lessong5sA1n1Printer.html#id7",
            "https://s3.amazonaws.com/novessa/lessons/lessong5sA1n1Printer.html#id8",
            "https://s3.amazonaws.com/novessa/lessons/lessong5sA1n1Printer.html#id9",
            "https://s3.amazonaws.com/novessa/lessons/lessong5sA1n1Printer.html#id10",
            "https://s3.amazonaws.com/novessa/lessons/lessong5sA1n1Printer.html#id11",
            "https://s3.amazonaws.com/novessa/lessons/lessong5sA1n1Printer.html#id12",
            "https://s3.amazonaws.com/novessa/lessons/lessong5sA1n1Printer.html#id13",
            "https://s3.amazonaws.com/novessa/lessons/lessong5sA1n1Printer.html#id14",
            "https://s3.amazonaws.com/novessa/lessons/lessong5sA1n1Printer.html#id15",
            "https://s3.amazonaws.com/novessa/lessons/lessong5sA1n1Printer.html#id16",
            "https://s3.amazonaws.com/novessa/lessons/lessong5sA1n1Printer.html#id17",
            "https://s3.amazonaws.com/novessa/lessons/lessong5sA1n1Printer.html#id18",
            "https://s3.amazonaws.com/novessa/lessons/lessong5sA1n1Printer.html#id19",
            "https://s3.amazonaws.com/novessa/lessons/lessong5sA1n1Printer.html#id20",
            "https://s3.amazonaws.com/novessa/lessons/lessong5sA1n1Printer.html#id21",
            "https://s3.amazonaws.com/novessa/lessons/lessong5sA1n1Printer.html#id22",
            "https://s3.amazonaws.com/novessa/lessons/lessong5sA1n1Printer.html#id23",
            "https://s3.amazonaws.com/novessa/lessons/lessong5sA1n1Printer.html#id24",
            "https://s3.amazonaws.com/novessa/lessons/lessong5sA1n1Printer.html#id25",
            "https://s3.amazonaws.com/novessa/lessons/lessong5sA1n1Printer.html#id26",
            "https://s3.amazonaws.com/novessa/lessons/lessong5sA1n1Printer.html#id27",
            "https://s3.amazonaws.com/novessa/lessons/lessong5sA1n1Printer.html#id28"
        };

        double[] nodeweights =
        {
            0.5,
            0.5,
            0.5,
            0.5,
            0.5,
            0.5,
            0.5,
            0.5,
            0.5,
            0.5,
            0.5,
            0.5,
            0.5,
            0.5,
            0.5,
            0.5,
            0.5,
            0.5,
            0.5,
            0.5,
            0.5,
            0.5,
            0.5,
            0.5,
            0.5,
            0.5,
            0.5,
            0.5
        };

        double gain = 0.05;
        double momentum = 0.05;
        double excitation = 2.5;
        double inhibition = -2.5;

        double documentweight = 0.5;

        double sum_node_weights;

        // Links to the latent dimensions (nodes with weight property)
        int[] links =
        {
            0,
            0,
            1,
            0,
            0,
            0,
            0,
            1,
            0,
            0,
            0,
            1,
            1,
            1,
            0,
            0,
            1,
            0,
            0,
            0,
            1,
            0,
            1,
            1,
            1,
            0,
            1,
            1
        };

        double[] dimensions =
        {
            0.5,
            0.5
        };

        int matchedelementindex;
        double matchedelementsimilarity = 0;
        List<int> intlistrecommendations = new List<int>();

        double largestweightvalue = 0;
        int largestweightindex;

        public string databasestring;

        private void Form1_Load(object sender, EventArgs e)
        {
            // Load image for pedagogical agent
            this.pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\Amy Animation 1.gif");
            timer2.Start();
            timer3.Start();
            synth.SelectVoiceByHints(VoiceGender.Female);
            synth.Volume = 50;
            synth.Rate = -2;
            synth.SetOutputToDefaultAudioDevice();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            webBrowser1.GoBack();
            //MessageBox.Show("Backward navigation event");
            // Log backward navigation event
            LogFile("Browser", "Navigation_Back", textBox1.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            webBrowser1.GoForward();
            //MessageBox.Show("Forward navigation event");
            // Log forward navigation event
            LogFile("Browser", "Navigation_Forward", textBox1.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string url = textBox1.Text; //Get url address from textbox

            webBrowser1.Navigate(url); //Navigate to the url address using the web browser
            LogFile("Browser", "Navigation_Refresh", textBox1.Text);
            //MessageBox.Show("Navigation event");
            // Log navigation event
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string url = textBox1.Text;

                webBrowser1.Navigate(url);
                LogFile("Browser", "Navigation_Refresh", textBox1.Text);
                //MessageBox.Show("Navigation event");
                // Log navigation event
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string url = "https://s3.amazonaws.com/novessa/lessons/lessong5sA1n1Printer.html";

            webBrowser1.Navigate(url); //Navigate to the url address using the web browser
            LogFile("Browser", "Navigation_Home", textBox1.Text);
            //MessageBox.Show("Navigation event");
            // Log navigation event
        }

        private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            textBox1.Text = webBrowser1.Url.AbsoluteUri;
            LogFile("Browser", "Navigation_Event", textBox1.Text);
            //MessageBox.Show(textBox1.Text);
            //Log url address of web navigation event
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            // Review button click - request support from Amy to assess the quality of the solution
            SolutionExplorerMonitor("review");
        }

        // This function is responsible for processing requests for new dialogue moves
        public void AmyDialogController(string request)
        {
            RefreshLessonPlan();
            //MessageBox.Show(interface_status);
            //MessageBox.Show(dialog_request);
            //MessageBox.Show(request);
            if (request == "setgoal" && fadedscaffold == "false")
            {
                // Set the dialog move in the messages box
                LogFile("Tutor","Amy_SetGoal", amy_setsubgoal);
                AmyDialogBuilder(amy_setsubgoal);

                // Set the current status of the dialog
                dialog_request = "setgoal";
            }else if(request == "amy_reviewsubgoal1")
            {
                LogFile("Tutor", "Amy_Review", amy_reviewsubgoal1);
                AmyDialogBuilder(amy_reviewsubgoal1);
            }
            else if (request == "amy_reviewsubgoal2")
            {
                LogFile("Tutor", "Amy_Review", amy_reviewsubgoal2);
                AmyDialogBuilder(amy_reviewsubgoal2);
            }else if(request == "informationseeking" && fadedscaffold == "false")
            {
                LogFile("Tutor", "Amy_InfoSeek", amy_informationseeking1);
                AmyDialogBuilder(amy_informationseeking1);
                Delayed(2000, () => AmyDialogBuilder(amy_informationseeking2));
                LogFile("Tutor", "Amy_InfoSeek", amy_informationseeking2);
                Delayed(5000, () => AmyDialogBuilder(amy_informationseeking3));
                LogFile("Tutor", "Amy_InfoSeek", amy_informationseeking3);
                Delayed(5000, () => AmyDialogBuilder(amy_informationseeking4));
                LogFile("Tutor", "Amy_InfoSeek", amy_informationseeking4);
            }
            else if(request == "amy_reviewinformation1")
            {
                LogFile("Tutor", "Amy_Review", amy_reviewinformation1);
                AmyDialogBuilder(amy_reviewinformation1);
            }
            else if (request == "amy_reviewinformation2")
            {
                LogFile("Tutor", "Amy_Review", amy_reviewinformation2);
                AmyDialogBuilder(amy_reviewinformation2);
            }
            else if (request == "amy_reviewinformation3")
            {
                LogFile("Tutor", "Amy_Review", amy_reviewinformation3);
                AmyDialogBuilder(amy_reviewinformation3);
            }
            else if (request == "amy_reviewinformation4")
            {
                LogFile("Tutor", "Amy_Review", amy_reviewinformation4);
                AmyDialogBuilder(amy_reviewinformation4);
            }
            else if (request == "amy_reviewinformation5")
            {
                LogFile("Tutor", "Amy_Review", amy_reviewinformation5);
                AmyDialogBuilder(amy_reviewinformation5);
            }
            else if (request == "amy_reviewinformation6")
            {
                LogFile("Tutor", "Amy_Review", amy_reviewinformation6);
                AmyDialogBuilder(amy_reviewinformation6);
            }
            else if (request == "amy_reviewinformation7")
            {
                LogFile("Tutor", "Amy_Review", amy_reviewinformation7);
                AmyDialogBuilder(amy_reviewinformation7);
            }
            else if (request == "solutionconstruction" && fadedscaffold == "false")
            {
                // You are here - add dialog from agent for task construction
                LogFile("Tutor", "Amy_SolutionConstruct", amy_taskconstruction1);
                AmyDialogBuilder(amy_taskconstruction1);
                Delayed(22000, () => AmyDialogBuilder(amy_taskconstruction2));
                LogFile("Tutor", "Amy_SolutionConstruct", amy_taskconstruction2);
            }
            else if (request == "amy_reviewtaskconstruction1")
            {
                LogFile("Tutor", "Amy_Review", amy_reviewtaskconstruction1);
                AmyDialogBuilder(amy_reviewtaskconstruction1);
            }
            else if (request == "amy_reviewtaskconstruction2")
            {
                LogFile("Tutor", "Amy_Review", amy_reviewtaskconstruction2);
                AmyDialogBuilder(amy_reviewtaskconstruction2);
            }
            else if (request == "amy_reviewtaskconstruction3")
            {
                LogFile("Tutor", "Amy_Review", amy_reviewtaskconstruction3);
                AmyDialogBuilder(amy_reviewtaskconstruction3);
            }
            else if (request == "amy_reviewtaskconstruction4")
            {
                LogFile("Tutor", "Amy_Review", amy_reviewtaskconstruction4);
                AmyDialogBuilder(amy_reviewtaskconstruction4);
            }
            else if (request == "solutionassessment" && fadedscaffold == "false")
            {
                // You are here - add dialog from agent for task solution assessment
                LogFile("Tutor", "Amy_SolutionAssess", amy_solutionassessment1);
                AmyDialogBuilder(amy_solutionassessment1);
                Delayed(5000, () => AmyDialogBuilder(amy_solutionassessment2));
                LogFile("Tutor", "Amy_SolutionAssess", amy_solutionassessment2);
                Delayed(5000, () => AmyDialogBuilder(amy_solutionassessment3));
                LogFile("Tutor", "Amy_SolutionAssess", amy_solutionassessment3);
                Delayed(5000, () => AmyDialogBuilder(amy_solutionassessment4));
                LogFile("Tutor", "Amy_SolutionAssess", amy_solutionassessment4);
            }
            else if (request == "amy_reviewassessment1")
            {
                LogFile("Tutor", "Amy_Review", amy_reviewassessment1);
                AmyDialogBuilder(amy_reviewassessment1);
            }
            else if (request == "independentpractice" && fadedscaffold == "false")
            {
                LogFile("Tutor", "Amy_FadePrompt", amy_independentpractice);
                AmyDialogBuilder(amy_independentpractice);
                fadedscaffold = "true";
            }
        }

        // This function is responsible for building the dialog of the agent in the messages box
        public void AmyDialogBuilder(string response)
        {
            synth.SpeakAsync(response);
            richTextBox1.AppendText("\nAmy: " + response + "\n");
            Task task1 = new Task(new Action(scroll));
            task1.Start();
        }

        void scroll()
        {
            this.Invoke(new MethodInvoker(delegate ()
            {
                richTextBox1.SelectionStart = richTextBox1.Text.Length;
                richTextBox1.ScrollToCaret();
            }));
        }

        // This function is responsible for getting requests to update the user interface
        public void SolutionExplorerController(string request)
        {
            RefreshLessonPlan();
            //MessageBox.Show(interface_status);
            //MessageBox.Show(dialog_request);
            //MessageBox.Show(request);
            if (request == "setgoal")
            {
                // Set the solution explorer to allow a user to set a goal
                splitContainer7.Panel1Collapsed = false;
                splitContainer7.Panel2Collapsed = true;
                splitContainer2.Panel1Collapsed = false;
                splitContainer2.Panel2Collapsed = true;
                richTextBox3.Text = learning_objective;
                LogFile("Objective", "GoalSet", richTextBox3.Text);
                richTextBox7.Text = learning_objective;
                string url = "https://s3.amazonaws.com/novessa/lessons/lessong5sA1n1Printer.html";
                webBrowser1.Navigate(url); //Navigate to the url address using the web browser
                button7.Enabled = true;
                button10.Enabled = false;

                // Set the current status of the user interface
                interface_status = "setgoal";
            }else if (request == "informationseeking")
            {
                // Set the solution explorer to allow a user to seek and acquire information
                splitContainer7.Panel1Collapsed = false;
                splitContainer7.Panel2Collapsed = true;
                splitContainer2.Panel1Collapsed = true;
                splitContainer2.Panel2Collapsed = false;
                splitContainer3.Panel1Collapsed = false;
                splitContainer3.Panel2Collapsed = true;
                button5.Enabled = true;
                timer1.Start();

                // Set the current status of the user interface
                interface_status = "informationseeking";
            }else if (request == "solutionconstruction")
            {
                // Set the solution explorer to allow a user to build a solution for the task
                splitContainer7.Panel1Collapsed = false;
                splitContainer7.Panel2Collapsed = true;
                splitContainer2.Panel1Collapsed = true;
                splitContainer2.Panel2Collapsed = false;
                splitContainer3.Panel1Collapsed = true;
                splitContainer3.Panel2Collapsed = false;
                splitContainer4.Panel1Collapsed = false;
                splitContainer4.Panel2Collapsed = true;
                button5.Enabled = false;
                button6.Enabled = true;

                // Set the current status of the user interface
                interface_status = "solutionconstruction";
            }else if (request == "solutionassessment")
            {
                // Set the solution explorer to allow a user to assess the quality of the solution for the task
                splitContainer7.Panel1Collapsed = true;
                splitContainer7.Panel2Collapsed = false;
                splitContainer2.Panel1Collapsed = true;
                splitContainer2.Panel2Collapsed = false;
                splitContainer3.Panel1Collapsed = true;
                splitContainer3.Panel2Collapsed = false;
                splitContainer4.Panel1Collapsed = true;
                splitContainer4.Panel2Collapsed = false;
                splitContainer5.Panel1Collapsed = false;
                splitContainer5.Panel2Collapsed = true;
                button6.Enabled = false;
                button10.Enabled = true;
                button21.Enabled = true;

                // Set the current status of the user interface
                interface_status = "solutionassessment";
            }
        }

        // This function is responsible for getting the user inputs from the solution explorer and sending info to the controller
        public void SolutionExplorerMonitor(string request)
        {
            //MessageBox.Show(interface_status);
            //MessageBox.Show(dialog_request);
            //MessageBox.Show(request);
            if(request == "review")
            {
                if(interface_status == "setgoal")
                {
                    GetSubGoals();
                }else if(interface_status == "informationseeking")
                {
                    GetInformation();
                }else if(interface_status == "solutionconstruction")
                {
                    GetTaskSolution();
                }else if(interface_status == "solutionassessment")
                {
                    GetSolutionAssessment();
                }
            }
            else
            {
                MessageBox.Show("Request not recognized for solution explorer monitor module");
            }

        }

        // Functions to get values from input
        public void GetSubGoals()
        {

            if (comboBox1.SelectedIndex == 0)
            {
                task = "assess"; // assess is set
                // Log third task is selected - assess student understanding
                SetSubGoals();

                GetTaskTime();
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                task = "demonstrate"; // demonstrate is set
                // Log third task is selected - demonstrate concept
                SetSubGoals();

                GetTaskTime();
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                task = "reflect"; // reflect is set
                // Log third task is selected - foster reflection
                SetSubGoals();

                GetTaskTime();

            }
            else
            {
                // Log unrecognized task index selected
                AmyDialogController("amy_reviewsubgoal1");
            }

        }

        public void GetTaskTime()
        {
            tasktime = (int)numericUpDown1.Value;
            if (tasktime == 0)
            {
                // Log no time allocated to task
                AmyDialogController("amy_reviewsubgoal2");
            }
            else
            {
                // Log time amount allocated to task
                AmyDialogController("informationseeking");
                SolutionExplorerController("informationseeking");
            }
        }

        public void GetInformation()
        {
            // Get the value of the type of lesson activity
            if (comboBox3.SelectedIndex == 0)
            {
                activitytype = "demonstrate"; // present is set
                // Log first activity is selected - demonstrate concept
                GetActivityInformation();
            }
            else if (comboBox3.SelectedIndex == 1)
            {
                activitytype = "assess"; // assess is set
                // Log second activity is selected - assess student understanding
                GetActivityInformation();
            }
            else if (comboBox3.SelectedIndex == 2)
            {
                activitytype = "reflect"; // reflect is set
                // Log third activity is selected - foster reflection
                GetActivityInformation();
            }
            else
            {
                // Log unrecognized activity index selected
                AmyDialogController("amy_reviewinformation1");
            }

        }

        public void GetActivityInformation()
        {
            // Get the value of the description of the lesson activity
            activitylabel = textBox4.Text;

            // Get the value of the activity description
            activitydescription = richTextBox2.Text;

            // Get the subject matter of the activity
            activitysubject = this.comboBox5.GetItemText(this.comboBox5.SelectedItem);

            // Get the grade level of the activity
            activitylevel = this.comboBox6.GetItemText(this.comboBox6.SelectedItem);

            // Get the relevant standards of the activity
            activitystandard = this.comboBox7.GetItemText(this.comboBox7.SelectedItem);

            // Get the url address of the activity
            activityurl = textBox5.Text;

            if(activitylabel == "" || activitylabel == "Add a brief title for the activity")
            {
                AmyDialogController("amy_reviewinformation2");
            }
            else
            {
                if(activitydescription == "" || activitydescription == "Write a description of the lesson activity")
                {
                    AmyDialogController("amy_reviewinformation3");
                }
                else
                {
                    if (activitysubject == "")
                    {
                        AmyDialogController("amy_reviewinformation4");
                    }
                    else
                    {
                        if (activitylevel == "")
                        {
                            AmyDialogController("amy_reviewinformation5");
                        }
                        else
                        {
                            if (activitystandard == "")
                            {
                                AmyDialogController("amy_reviewinformation6");
                            }
                            else
                            {
                                if (activityurl == "")
                                {
                                    AmyDialogController("amy_reviewinformation7");
                                }
                                else
                                {
                                    // Spread activation through the network based on the written description of the activity

                                    // Reset the development environment (the similarity measures are refreshed in a seperate function
                                    RefreshDevelopmentEnvironment();
                                    // Match all elements to the currently written description of the activity in the solution explorer
                                    nodematching(richTextBox2.Text);
                                    // Sort the similarity values while excluding largest values based on prior recommendations (largest and unique)
                                    findlargestmatch();
                                    // Spread activation through the network based on the matched node
                                    SpreadingActivation(matchedelementindex);
                                    
                                    // The development environment is refreshed with the new values for node weights
                                    RefreshDevelopmentEnvironment();

                                    AmyDialogController("solutionconstruction");
                                    SolutionExplorerController("solutionconstruction");
                                }
                            }
                        }
                    }
                }
            }
        }

        public void GetTaskSolution()
        {
            techname = textBox6.Text;
            techsource = textBox7.Text;
            techaffordance = textBox8.Text;
            techjustification = richTextBox11.Text;

            if (techname == "" || techname == "Add the name of the technology")
            {
                AmyDialogController("amy_taskconstruction1");
            }
            else
            {
                if(techsource == "" || techsource == "Add a URL to the website")
                {
                    AmyDialogController("amy_taskconstruction2");
                }
                else
                {
                    if(techaffordance == "" || techaffordance == "Identify the type of affordance (see below)")
                    {
                        AmyDialogController("amy_taskconstruction3");
                    }
                    else
                    {
                        if(techjustification == "" || techjustification == "Write a brief justification for why this technology provides the affordances stated in the context of the activity")
                        {
                            AmyDialogController("amy_taskconstruction4");
                        }
                        else
                        {
                            AmyDialogController("solutionassessment");
                            SolutionExplorerController("solutionassessment");
                        }
                    }
                }
            }
        }

        public void GetSolutionAssessment()
        {
            criteria1 = this.comboBox2.GetItemText(this.comboBox2.SelectedItem);
            criteria2 = this.comboBox4.GetItemText(this.comboBox4.SelectedItem);
            criteria3 = this.comboBox8.GetItemText(this.comboBox8.SelectedItem);
            criteria4 = this.comboBox9.GetItemText(this.comboBox9.SelectedItem);
            criteria5 = this.comboBox10.GetItemText(this.comboBox10.SelectedItem);
            criteria6 = this.comboBox11.GetItemText(this.comboBox11.SelectedItem);
            criteria7 = this.comboBox12.GetItemText(this.comboBox12.SelectedItem);
            criteria8 = this.comboBox13.GetItemText(this.comboBox13.SelectedItem);

            if(criteria1 == "")
            {
                AmyDialogController("amy_reviewassessment1");
            }
            else if(criteria2 == "")
            {
                AmyDialogController("amy_reviewassessment1");
            }
            else if(criteria3 == "")
            {
                AmyDialogController("amy_reviewassessment1");
            }
            else if(criteria4 == "")
            {
                AmyDialogController("amy_reviewassessment1");
            }
            else if(criteria5 == "")
            {
                AmyDialogController("amy_reviewassessment1");
            }
            else if(criteria6 == "")
            {
                AmyDialogController("amy_reviewassessment1");
            }
            else if(criteria7 == "")
            {
                AmyDialogController("amy_reviewassessment1");
            }
            else if(criteria8 == "")
            {
                AmyDialogController("amy_reviewassessment1");
            }
            else
            {
                AmyDialogController("independentpractice");
                SolutionExplorerController("setgoal");
                
            }
        }

        // Functions to set values in user interface
        public void SetSubGoals()
        {
            checkedListBox1.Items.Clear();
            if(task == "assess")
            {
                checkedListBox1.Items.Add("Look for an activity where the teacher assesses student understanding");
                LogFile("Tasks", "SubGoalSet", "Look for an activity where the teacher assesses student understanding");
                checkedListBox1.Items.Add("Highlight the relevant information from the lesson");
                LogFile("Tasks", "SubGoalSet", "Highlight the relevant information from the lesson");
                checkedListBox1.Items.Add("Elaborate upon the information by incorporating the use of technology");
                LogFile("Tasks", "SubGoalSet", "Elaborate upon the information by incorporating the use of technology");
                checkedListBox1.Items.Add("Appraise the revised lesson activity");
                LogFile("Tasks", "SubGoalSet", "Appraise the revised lesson activity");
                comboBox3.SelectedIndex = 1;
            }
            else if(task == "demonstrate")
            {
                checkedListBox1.Items.Add("Look for an activity where the teacher demonstrates a topic");
                LogFile("Tasks", "SubGoalSet", "Look for an activity where the teacher demonstrates a topic");
                checkedListBox1.Items.Add("Highlight the relevant information from the lesson");
                LogFile("Tasks", "SubGoalSet", "Highlight the relevant information from the lesson");
                checkedListBox1.Items.Add("Elaborate upon the information by incorporating the use of technology");
                LogFile("Tasks", "SubGoalSet", "Elaborate upon the information by incorporating the use of technology");
                checkedListBox1.Items.Add("Appraise the revised lesson activity");
                LogFile("Tasks", "SubGoalSet", "Appraise the revised lesson activity");
                comboBox3.SelectedIndex = 0;
            }
            else if(task == "reflect")
            {
                checkedListBox1.Items.Add("Look for an activity where the teacher initiates student reflection");
                LogFile("Tasks", "SubGoalSet", "Look for an activity where the teacher initiates student reflection");
                checkedListBox1.Items.Add("Highlight the relevant information from the lesson");
                LogFile("Tasks", "SubGoalSet", "Highlight the relevant information from the lesson");
                checkedListBox1.Items.Add("Elaborate upon the information by incorporating the use of technology");
                LogFile("Tasks", "SubGoalSet", "Elaborate upon the information by incorporating the use of technology");
                checkedListBox1.Items.Add("Appraise the revised lesson activity");
                LogFile("Tasks", "SubGoalSet", "Appraise the revised lesson activity");
                comboBox3.SelectedIndex = 2;
            }
            else
            {
                // Log task value not set
            }
        }

        // Timer Method to Introduce Delay
        public void Delayed(int delay, Action action)
        {
            Timer timer = new Timer();
            timer.Interval = delay;
            timer.Tick += (s, e) => {
                action();
                timer.Stop();
            };
            timer.Start();
        }

        // Recommender system that is based on the network-based domain model
        public void RecommenderSystem()
        {
            // Generate the recommendation to the learner based on this index value
            findlargestweightvalue();
            // Amy generates a recommendation based on the analysis of the network properties and past suggestions (to avoid duplicates)
            LogFile("Tutor","Amy_Suggestion", amy_recommendation);
            AmyDialogBuilder(amy_recommendation);
            webBrowser1.Navigate(urls[largestweightindex]);
            intlistrecommendations.Add(largestweightindex);
        }

        // Hint delivery system to support help seeking behaviors and navigate the tech integration scaffold
        public void HintDeliverySystem()
        {
            if(task == "demonstrate")
            {
                if(hintlevel == "level1")
                {
                    hintcontent = "In the Mathematics Learning Activity types, examine activities from the ‘Consider’ and ‘Produce’ categories.";
                    LogFile("Tutor", "Amy_Assistance", hintcontent);
                    AmyDialogBuilder(hintcontent);
                    hintlevel = "level2";
                }
                else if(hintlevel == "level2")
                {
                    hintcontent = "Elaborate further on an activity from the ‘Consider’ category labeled as ‘Attend to a Demonstration’.";
                    LogFile("Tutor", "Amy_Assistance", hintcontent);
                    AmyDialogBuilder(hintcontent);
                }
                else
                {
                    // Log hint request not recognized because of unknown level of hint specificity
                }
            }
            else if(task == "assess")
            {
                if (hintlevel == "level1")
                {
                    hintcontent = "In the Mathematics Learning Activity types, examine activities from the ‘Apply’, ‘Evaluate’, and ‘Practice’ categories.";
                    LogFile("Tutor", "Amy_Assistance", hintcontent);
                    AmyDialogBuilder(hintcontent);
                    hintlevel = "level2";
                }
                else if (hintlevel == "level2")
                {
                    hintcontent = "Elaborate further on an activity from the ‘Apply’ category labeled as ‘Take a Test’.";
                    LogFile("Tutor", "Amy_Assistance", hintcontent);
                    AmyDialogBuilder(hintcontent);
                }
                else
                {
                    // Log hint request not recognized because of unknown level of hint specificity
                }
            }
            else if(task == "reflect")
            {
                if (hintlevel == "level1")
                {
                    hintcontent = "In the Mathematics Learning Activity types, examine activities from the ‘Interpret’ and ‘Create’ categories.";
                    LogFile("Tutor", "Amy_Assistance", hintcontent);
                    AmyDialogBuilder(hintcontent);
                    hintlevel = "level2";
                }
                else if (hintlevel == "level2")
                {
                    hintcontent = "Elaborate further on the activity from the ‘Interpret’ category labeled as ‘Create a Process’.";
                    LogFile("Tutor", "Amy_Assistance", hintcontent);
                    AmyDialogBuilder(hintcontent);
                }
                else
                {
                    // Log hint request not recognized because of unknown level of hint specificity
                }
            }
            else
            {
                // Log hint request not recognized because of unknown task
            }
        }

        // Event Handlers

        private void sessionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(toolStripTextBox1.Text == "" || toolStripTextBox1.Text == "User ID")
            {
                MessageBox.Show("Enter username and condition prior to beginning the study.");
            }
            else
            {
                // User initiates a new session - set the interface features and the learning objective to setgoal
                RefreshDevelopmentEnvironment();
                SolutionExplorerController("setgoal");
                AmyDialogController("setgoal");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox5.Text = textBox1.Text;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBox7.Text = textBox1.Text;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                string textselectednode = treeView1.SelectedNode.Text;
                LogFile("Solution_Explorer", "Tech_Hint", textselectednode);

                //MessageBox.Show(selectedhint);
                // Log selected hint event

                switch (textselectednode)
                {
                    case "Mathematics Learning Activity Types":
                        richTextBox4.Text = "Essentially, these mathematics activity types are designed to be catalysts to thoughtful and creative instruction by teachers. We have conceptualized seven genres of activity types for mathematics that are derived from the National Council of Teachers of Mathematics’ (NCTM’s) process standards. To encourage active engagement by all students, these activity types are expressed using active words (verbs) to focus instructional planning on student rather than teacher actions.  Many of these words are drawn directly from the NCTM standards.  Each of the seven genres is presented in a separate table that names the activity types for that genre, briefly defines them, and then provides some example technologies that might be selected by a teacher while undertaking each activity. Please note that the specific software titles referenced in the Possible Technologies columns are meant to be illustrative.\n\n1 Grandgenett, N., Harris, J., & Hofer, M. (2011, February). Mathematics learning activity types. Retrieved from College of William and Mary, School of Education, Learning Activity Types: http://activitytypes.wm.edu/Math.html \n\n2   “Mathematics Learning Activity Types” by Neal Grandgenett, Judi Harris and Mark Hofer is licensed under a Creative Commons Attribution - Noncommercial - No Derivative Works 3.0 United States License.Based on a work at http://activitytypes.wm.edu. ";
                        break;
                    case "Consider":
                        richTextBox4.Text = "When learning mathematics, students are often asked to thoughtfully consider new concepts or information.  This request is a familiar one for the mathematics student, and is just as familiar to the teacher. Yet, although such learning activities can be very important contributors to student understanding, the 'Consider' activity types also often represent some of the lower levels of student engagement, and typically are manifested using a relatively direct presentation of foundational knowledge.";
                        break;
                    case "Attend to a Demonstration":
                        richTextBox4.Text = "Students gain information from a presentation, videoclip, animation, interactive whiteboard or other display media. \n\nDocument camera, Content-Specific interactive tool (e.g., ExploreMath) presentation or video creation software, video clips, videoconferencing.";
                        break;
                    case "Read Text":
                        richTextBox4.Text = "Students extract information from textbooks or other written materials, in either print or digital form. \n\nElectronic textbook, websites (i.e. the Math Forum), informational electronic documents (e.g. .pdfs).";
                        break;
                    case "Discuss":
                        richTextBox4.Text = "Students discuss a concept or process with a teacher, other students, or an external expert.\n\nAsk-an-expert site (e.g., Ask Dr. Math), online discussion group, videoconferencing.";
                        break;
                    case "Recognize a Pattern":
                        richTextBox4.Text = "Students examine a pattern presented to them and attempt to understand the pattern better.\n\nGraphing calculators, virtual manipulative site (e.g., the National Library of Virtual Manipulatives), content-specific interactive tool (e.g., ExploreMath), spreadsheet.";
                        break;
                    case "Investigate a Concept":
                        richTextBox4.Text = "Students explore or investigate a concept (such as fractals), perhaps by use of the Internet or other research-related resources.\n\nContent-Specific interactive tool (e.g., ExploreMath), Web searching, informational databases (e.g., Wikipedia), virtual worlds (e.g., Second Life), simulations.";
                        break;
                    case "Understand or Define a Problem":
                        richTextBox4.Text = "Students strive to understand the context of a stated problem or to define the mathematical characteristics of a problem.\n\nWeb searching, concept mapping software, ill-structured problem media (e.g., CIESE Projects).";
                        break;
                    case "Practice":
                        richTextBox4.Text = "In the learning of mathematics, it is often very important for a student to be able to practice computational techniques or other algorithm-based strategies, in order to automate these skills for later and higher-level mathematical applications. Some educational technologies can provide valuable assistance in helping students to practice and internalize important skills and techniques.  This table provides some examples of how technology can assist in these important student practice efforts.";
                        break;
                    case "Do Computation":
                        richTextBox4.Text = "Students undertake computation-based strategies using numeric or symbolic processing.\n\nScientific calculators, graphing calculators, spreadsheet, Mathematica.";
                        break;
                    case "Do Drill and Practice":
                        richTextBox4.Text = "Students rehearse a mathematical strategy or technique, and perhaps uses computer-aided repetition and feedback in the practice process.\n\nDrill and practice software, online textbook supplement, online homework help websites (e.g., WebMath).";
                        break;
                    case "Solve a Puzzle":
                        richTextBox4.Text = "Students carry out a mathematical strategy or technique within the context of solving an engaging puzzle, which may be facilitated or posed by the technology.\n\nVirtual manipulative, Web-based puzzle (e.g., magic squares), mathematical brainteaser Web site (e.g., CoolMath).";
                        break;
                    case "Interpret":
                        richTextBox4.Text = "In the discipline of mathematics, individual concepts and relationships can be quite abstract, and at times can even represent a bit of a mystery to students.  Often students need to spend some time deducing and explaining these relationships to internalize them.  Educational technologies can be used to help students investigate concepts and relationships more actively, and assist them in interpreting what they observe.  This table displays activity types that can support this thoughtful interpretation process, and provides some examples of the available technologies that can be used to support forming the interpretations.";
                        break;
                    case "Pose a Conjecture":
                        richTextBox4.Text = "The student poses a conjecture, perhaps using dynamic software to display relationships.\n\nDynamic geometry software (e.g., Geometer’s Sketchpad), Content-specific interactive tool (e.g., ExploreMath), e-mail.";
                        break;
                    case "Develop an Argument":
                        richTextBox4.Text = "The student develops a mathematical argument related to why they think that something is true.  Technology may help to form and to display that argument.\n\nConcept mapping software, presentation software, blog, specialized word processing software (e.g., Theorist).";
                        break;
                    case "Categorize":
                        richTextBox4.Text = "The student attempts to examine a concept or relationship in order to categorize it into a set of known categories.\n\nDatabase software, online database, concept mapping software, drawing software.";
                        break;
                    case "Interpret a Representation":
                        richTextBox4.Text = "The student explains the relationships apparent from a mathematical representation (table, formula, chart, diagram, graph, picture, model, animation, etc.).\n\nData visualization software (e.g., Inspire Data), 2D and 3D animation, video clip, Global Positioning Devices (GPS), engineering-related visualization software (e.g., MathCad).";
                        break;
                    case "Estimate":
                        richTextBox4.Text = "The student attempts to approximate some mathematical value by further examining relationships using supportive technologies.\n\nScientific calculator, graphing calculator, spreadsheet, student response system (e.g. “clickers”).";
                        break;
                    case "Interpret a Phenomenon Mathematically":
                        richTextBox4.Text = "Assisted by technology as needed, the student examines a mathematics-related phenomenon (such as velocity, acceleration, the Golden Ratio, gravity, etc.).\n\nDigital camera, video, computer-aided laboratory equipment, engineering-related visualization software, specialized word processing software (e.g., Theorist), robotics, electronics kit.";
                        break;
                    case "Produce":
                        richTextBox4.Text = "When students are actively engaged in the study of mathematics, they can become motivated producers of mathematical works, rather than just passive consumers of prepared materials.  Educational technologies can serve as excellent “partners” in this production process, aiding in the refinement and formalization of a student product, as well as helping the student to share the fruits of their mathematical labors.  The activity types listed below suggest technology-assisted efforts in which students become “producers” of mathematics-related products.";
                        break;
                    case "Do a Demonstration":
                        richTextBox4.Text = "The student makes a demonstration on some topic to show their understanding of a mathematical idea or process.  Technology may assist in the development or presentation of the product.\n\nInteractive whiteboard, video creation software,  document camera, presentation software, podcast, video sharing site.";
                        break;
                    case "Generate Text":
                        richTextBox4.Text = "The student produces a report, annotation, explanation, journal entry or document, to illustrate their understanding.\n\nSpecialized word processing software (e.g., Math Type), collaborative word processing software, blog, online discussion group.";
                        break;
                    case "Describe an Object or Concept Mathematically":
                        richTextBox4.Text = "Assisted by the technology in the description or documentation process, the student produces a mathematical explanation of an object or concept.\n\nLogo graphics, engineering visualization software, concept mapping software, specialized word processing software, Mathematica.";
                        break;
                    case "Produce a Representation":
                        richTextBox4.Text = "Using technology for production assistance if appropriate, the student develops a mathematical representation (table, formula, chart, diagram, graph, picture, model, animation, etc.).\n\nSpreadsheet, virtual manipulatives (e.g., digital geoboard), document camera, concept mapping software, graphing calculator.";
                        break;
                    case "Develop a Problem":
                        richTextBox4.Text = "The student poses a mathematical problem that is illustrative of some mathematical concept, relationship, or investigative question.\n\nWord processing software, online discussion group, Wikipedia, Web searching, e-mail.";
                        break;
                    case "Apply":
                        richTextBox4.Text = "The utility of mathematics in the world can be found in its authentic application.  Educational technologies can be used to help students to apply their mathematical knowledge in the real world, and to link specific mathematical concepts to real world phenomena.  The technologies essentially become students’ assistants in their mathematical work, helping them to link the mathematical concepts being studied to the reality in which they live.";
                        break;
                    case "Choose a Strategy":
                        richTextBox4.Text = "The student reviews or selects a mathematics-related strategy for a particular context or application.\n\nOnline help sites (e.g., WebMath, Math Forum), Inspire Data, dynamic geometry/algebra software (e.g., Geometry Expressions), Mathematica, MathCAD.";
                        break;
                    case "Take a Test":
                        richTextBox4.Text = "The student demonstrates their mathematical knowledge within the context of a testing environment, such as with computer-assisted testing software.\n\nTest-taking software, Blackboard, online survey software, student response system (e.g. “clickers”).";
                        break;
                    case "Apply a Representation":
                        richTextBox4.Text = "The student applies a mathematical representation to a real life situation (table, formula, chart, diagram, graph, picture, model, animation, etc.).\n\nSpreadsheet, robotics, graphing calculator, computer-aided laboratories, virtual manipulatives (e.g., electronic algebra tiles).";
                        break;
                    case "Evaluate":
                        richTextBox4.Text = "When students evaluate the mathematical work of others, or self-evaluate their own mathematical work, they engage in a relatively sophisticated effort to try to understand mathematical concepts and processes.  Educational technologies can become valuable allies in this effort, assisting students in the evaluation process by helping them to undertake concept comparisons, test solutions or conjectures, and/or integrate feedback from other individuals into revisions of their work.  The following table lists some of these evaluation-related activities.";
                        break;
                    case "Compare and Contrast":
                        richTextBox4.Text = "The student compares and contrasts different mathematical strategies or concepts, to see which is more appropriate for a particular situation.\n\nConcept mapping software (e.g., Inspiration), Web searches, Mathematica, MathCad.";
                        break;
                    case "Test a Solution":
                        richTextBox4.Text = "The student systematically tests a solution, and examines whether it makes sense based upon systematic feedback, which might be assisted by technology.\n\nScientific calculator, graphing calculator, spreadsheet, Mathematica, Geometry Expressions.";
                        break;
                    case "Test a Conjecture":
                        richTextBox4.Text = "The student poses a specific conjecture and then examines the feedback of any interactive results to potentially refine the conjecture.\n\nGeometer Sketchpad, content-specific interactive tool (e.g., ExploreMath), statistical packages (e.g., SPSS, Fathom), online calculator, robotics.";
                        break;
                    case "Evaluate Mathematical Work":
                        richTextBox4.Text = "The student evaluates a body of mathematical work, through the use of peer or technology-aided feedback.\n\nOnline discussion group, blog, Mathematica, MathCad, Inspire Data.";
                        break;
                    case "Create":
                        richTextBox4.Text = "When students are involved in some of the highest levels of mathematics learning activities, they are often engaged in very creative and imaginative thinking processes. Albert Einstein once suggested that “imagination is more important than knowledge.”  It is said that this quote represents his strong belief that mathematics is a very inventive, inspired, and imaginative endeavor.  Educational technologies can be used to help students to be creative in their mathematical work, and even to help other students to deepen their learning of the mathematics that they already understand.  The activity types below represent these creative elements and processes in students’ mathematical learning and interaction.";
                        break;
                    case "Teach a Lesson":
                        richTextBox4.Text = "The student develops and delivers a lesson on a particular mathematics concept, strategy, or problem.\n\nDocument camera, presentation software, videoconferencing, video creation software, podcast.";
                        break;
                    case "Create a Plan":
                        richTextBox4.Text = "The student develops a systematic plan to address some mathematical problem or task.\n\nConcept mapping software, collaborative word processing software, MathCad, Mathematica.";
                        break;
                    case "Create a Product":
                        richTextBox4.Text = "The student imaginatively engages in the development of a student project, invention, or artifact, such as a new fractal, a tessellation, or another creative product.\n\nWord processing software, videocamera, animation tools, MathCad, Mathematica, Geometer Sketchpad.";
                        break;
                    case "Create a Process":
                        richTextBox4.Text = "The student creates a mathematical process that others might use, test or replicate, essentially engaging in mathematical creativity.\n\nComputer programming, robotics, Mathematica, MathCad, Inspire Data, video creation software.";
                        break;
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            
        }

        // Peer comparison system o support assessment of the solution quality
        public void PeerComparisonSystem()
        {
            examplepeerlesson = "Adding Like Fractions with Plickers (2 minutes) \n\nNote: This fluency activity reviews adding like units and lays the foundation for today’s task of adding unlike units. Plickers is a technology that allows to scan student answers submitted via cards that are rotated by the students to display their answer. The answers are logged by the system and displayed in front of the class in the form of a bar chart. \n\nT: Let’s add fractions mentally. Submit your answers from the list of available options as whole numbers when possible using the cards. \n\nT: 1/3 + 1/3 = ?\n\nS: 2/3.\n\nNote: Show the display with the bar chart in front of class.\n\nT: 1/4 + 1/4 = ?\n\nS: 2/4.\n\nNote: Show the display with the bar chart in front of class.\n\nT: 1/5 + 2/5 = ?\n\nS: 3/5.\n\nNote: Show the display with the bar chart in front of class.\n\nT: 3/7 + 4/7 = ?\n\nS: 1.\n\nNote: Show the display with the bar chart in front of class.\n\nT: 1/4 + 1/3 + 3/4 + 2/3 = ?\n\nS: 2.\n\nNote: Show the display with the bar chart in front of class.\n\nContinue and adjust to meet student needs by explaining why specific answers were obtained when incorrect answers are displayed in the bar chart.";
            richTextBox8.Text = examplepeerlesson;
            LogFile("Tutor", "Amy_Assessment", amy_peercomparison);
            AmyDialogBuilder(amy_peercomparison);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            richTextBox6.Text = "3: An object has superior alignment only if both of the following are true: All of the content and performance expectations in the identified standard are completely addressed by the object. The content and performance expectations of the identified standard are the focus of the object. While some objects may cover a range of standards that could potentially be aligned, for a superior alignment the content and performance expectations must not be a peripheral part of the object.\n\n2: An object has strong alignment for either one of two reasons: Minor elements of the standard are not addressed in the object. The content and performance expectations of the standard align to a minor part of the object.\n\n1: An object has limited alignment if a significant part of the content or performance expectations of the identified standard is not addressed in the object, as long as there is fidelity to the part it does cover. For example, an object that aligns to CCSS 2.NBT.2, “Count within 1000; skip - count by 5s, 10s, and 100s,” but only addresses counting numbers to 500, would be considered to have limited alignment. The object aligns very closely with a limited part of the standard.\n\n0: An object has very weak alignment for either one of two reasons: The object does not match the intended standards. The object matches only to minimally important aspects of a standard. These objects will not typically be useful for instruction of core concepts and performances covered by the standard.\n\nN / A: This rubric does not apply for an object that has no suggested standards for alignment. For example, the rubric might not be applicable to a set of raw data. ";
            //Log Link 1
            LogFile("Solution_Explorer", "Review_Criteria1", richTextBox6.Text);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            richTextBox6.Text = "3: An object is rated superior for explanation of subject matter only if all of the following are true: The object provides comprehensive information so effectively that the target audience should be able to understand the subject matter. The object connects important associated concepts within the subject matter. For example, a lesson on multi - digit addition makes connections with place value, rather than simply showing how to add multi - digit numbers. Or a lesson designed to analyze how an author develops ideas across extended text would make connections among the various developmental steps and the various purposes the author has for the text. The object does not need to be augmented with additional explanation or materials. The main ideas of the subject matter addressed in the object are clearly identified for the learner.\n\n2: An object is rated strong for explanation of subject matter if it explains the subject matter in a way that makes skills, procedures, concepts, and / or information understandable. It falls short of superior in that it does not make connections among important associated concepts within the subject matter. For example, a lesson on multi - digit addition may focus on the procedure and fail to connect it with place value.\n\n1: An object is rated limited for explanation of subject matter if it explains the subject matter correctly but in a limited way. This cursory treatment of the content is not sufficiently developed for a first-time learner of the content. The explanations are not thorough and would likely serve as a review for most learners.\n\n0: An object is rated very weak or no value for explanation of subject matter if its explanations are confusing or contain errors. There is little likelihood that this object will contribute to understanding.\n\nN / A: This rubric is not applicable (N / A) for an object that is not designed to explain subject matter, for example, a sheet of mathematical formulae or a map. It may be possible to apply the object in some way that aids a learner’s understanding, but that is beyond any obvious or described purpose of the object.";
            //Log Link 2
            LogFile("Solution_Explorer", "Review_Criteria2", richTextBox6.Text);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            richTextBox6.Text = "3: An object is rated superior for the utility of materials designed to support teaching only if all of the following are true: The object provides materials that are comprehensive and easy to understand and use. The object includes suggestions for ways to use the materials with a variety of learners. These suggestions include materials such as “common error analysis tips” and “precursor skills and knowledge” that go beyond the basic lesson or unit elements. All objects and all components are provided and function as intended and described. For example, the time needed for lesson planning appears accurately estimated, materials lists are complete, and explanations make sense. For larger objects like units, materials facilitate the use of a mix of instructional approaches(direct instruction, group work, investigations, etc.).\n\n2: An object is rated strong for the utility of materials designed to support teaching if it offers materials that are comprehensive and easy to understand and use but falls short of “superior” for either one of two reasons: The object does not include suggestions for ways to use the materials with a variety of learners (e.g., error analysis tips). Some core components (e.g., directions) are underdeveloped in the object.\n\n1: An object is rated limited for the utility of materials designed to support teaching if it includes a useful approach or idea to teach an important topic but falls short of “strong” for either one of two reasons: The object is missing important elements (e.g.directions for some parts of a lesson are not included). Important elements do not function as they are intended to(e.g.directions are unclear or practice exercises are missing or inadequate).Teachers would need to supplement this object to use it effectively.\n\n0: An object is rated very weak or no value for the utility of materials designed to support teaching if it is confusing, contains errors, is missing important elements, or is for some other reason simply not useful, in spite of an intention to be used as a support for teachers in planning or preparation.\n\nN / A: This rubric is not applicable (N / A) for an object that is not designed to support teachers in planning and / or presenting subject matter.It may be possible that an educator could find an application for such an object during a lesson, but that would not be the intended use";
            // Log Link 3
            LogFile("Solution_Explorer", "Review_Criteria3", richTextBox6.Text);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            richTextBox6.Text = "3: An object is rated superior for the quality of its assessments only if all of the following are true: All of the skills and knowledge assessed align clearly to the content and performance expectations intended, as stated or implied in the object. Nothing is assessed that is not included in the scope of intended material unless it is differentiated as extension material. The most important aspects of the expectations are targeted and are given appropriate weight/ attention in the assessment. The assessment modes used in the object, such as selected response, long and short constructed response, or group work require the student to demonstrate proficiency in the intended concept / skill. The level of difficulty is a result of the complexity of the subject-area content and performance expectations and of the degree of cognitive demand, rather than a result of unrelated issues(e.g.overly complex vocabulary used in math word problems).\n\n2: An object is rated strong for the quality of its assessments if it assesses all of the content and performance expectations intended, but the assessment modes used do not consistently offer the student opportunities to demonstrate proficiency in the intended concept / skill.\n\n1: An object is rated limited for the quality of its assessments if it assesses some of the content or performance expectations intended, as stated or implicit in the object, but omits some important content or performance expectations and/or fails to offer the student opportunities to demonstrate proficiency in the intended content/skills.\n\n0: An object is rated very weak or no value for the quality of its assessments if its assessments contain significant errors, do not assess important content/skills, are written in a way that is confusing to students, or are unsound for other reasons.\n\nN/A: This rubric is not applicable (N/A) for an object that is not designed to have an assessment component.Even if one might imagine ways an object could be used for assessment purposes, if it is not the intended purpose, not applicable is the appropriate score.";
            // Log Link 4
            LogFile("Solution_Explorer", "Review_Criteria4", richTextBox6.Text);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            richTextBox6.Text = "3: An object, or interactive component of an object, is rated superior for the quality of its technological interactivity only if all of the following are true: The object is responsive to student input in a way that creates an individualized learning experience. This means the object adapts to the user based on what s / he does, or the object allows the user some flexibility or individual control during the learning experience. The interactive element is purposeful and directly related to learning. The object is well - designed and easy to use, encouraging learner use. The object appears to function flawlessly on the intended platform.\n\n2: An object, or interactive component of an object, is rated strong for the quality of its technological interactivity if it has an interactive feature that is purposeful and directly related to learning, but does not provide an individualized learning experience.Similarly to the superior objects, strong interactive objects must be well designed, easy - to - use, and function flawlessly on the intended platform.Some technological elements may not be directly related to the content but for a strong rating they must not detract from the learning experience. These kinds of interactive elements, including earning points or achieving levels for correct answers, might be designed to increase student motivation and to build content understanding by rewarding or entertaining the learner, and may extend the time the user engages with the content.\n\n1: An object, or interactive component of an object, is rated limited for the quality of its technological interactivity if its interactive element does not relate to the subject matter and may detract from the learning experience. These kinds of interactive elements may slightly increase motivation but do not provide strong support for understanding the subject matter addressed in the object. It is unlikely that this interactive feature will increase understanding or extend the time a user engages with the content.\n\n0: An object, or interactive component of an object, is rated very weak or no value for the quality of its technological interactivity if it has interactive features that are poorly conceived and/ or executed. The interactive features might fail to operate as intended, distract the user, or unnecessarily take up user time. ";
            // Log Link 5
            LogFile("Solution_Explorer", "Review_Criteria5", richTextBox6.Text);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            richTextBox6.Text = "3: An object is rated superior for the quality of its instructional and practice exercises only if all of the following are true: The object offers more exercises than needed for the average student to facilitate mastery of the targeted skills, as stated or implied in the object. For complex tasks, one or two rich practice exercises may be considered more than enough. The exercises are clearly written and supported by accurate answer keys or scoring guidelines as applicable. There are a variety of exercise types and / or the exercises are available in a variety of formats, as appropriate to the targeted concepts and skills. For more complex practice exercises the formats used provide an opportunity for the learner to integrate a variety of skills.\n\n2: An object is rated strong for the quality of its instructional and practice exercises if it offers only a sufficient number of well - written exercises to facilitate mastery of targeted skills, which are supported by accurate answer keys or scoring guidelines, but there is little variety of exercise types or formats.\n\n1: An object is rated limited for the quality of its instructional and practice exercises if it has some, but too few exercises to facilitate mastery of the targeted skills, is without answer keys, and provides no variation in type or format.\n\n0: An object is rated very weak or no value for the quality of its instructional and practice exercises if the exercises provided do not facilitate mastery of the targeted skills, contain errors, or are unsound for other reasons.\n\nN / A: This rubric is not applicable (N / A) to an object that does not include opportunities to practice targeted skills.";
            // Log Link 6
            LogFile("Solution_Explorer", "Review_Criteria6", richTextBox6.Text);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            richTextBox6.Text = "3: An object is rated superior for its opportunities for deeper learning only if all of the following are true: At least three of the deeper learning skills from the list identified in this rubric are required in the object. The object offers a range of cognitive demand that is appropriate and supportive of the material. Appropriate scaffolding and direction are provided.\n\n2: An object is rated strong for its opportunities for deeper learning if it includes one or two deeper learning skills identified in this rubric. For example, the object might involve a complex problem that requires abstract reasoning skills to reach a solution.\n\n1: An object is rated limited for its opportunities for deeper learning if it includes one deeper learning skill identified in the rubric but is missing clear guidance on how to tap into the various aspects of deeper learning. For example, an object might include a provision for learners to collaborate, but the process and product are unclear.\n\n0: An object is rated very weak for its opportunities for deeper learning if it appears to be designed to provide some of the deeper learning opportunities identified in this rubric, but it is not useful as it is presented. For example, the object might be based on poorly formulated problems and/or unclear directions, making it unlikely that this lesson or activity will lead to skills like critical thinking, abstract reasoning, constructing arguments, or modeling.\n\nN/A: This rubric is not applicable(N/A) to an object that does not appear to be designed to provide the opportunity for deeper learning, even though one might imagine how it could be used to do so.";
            // Log Link 7
            LogFile("Solution_Explorer", "Review_Criteria7", richTextBox6.Text);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            richTextBox6.Text = "This rubric is used to assure materials are accessible to all students, including students identified as blind, visually impaired or print disabled, and those students who may qualify under the Chafee Amendment to the U.S. 1931 Act to Provide Books to the Adult Blind as Amended. It was developed to assess compliance with U.S.standards and requirements, but could be adapted to accommodate differences in other sets of requirements internationally. Accessibility is critically important for all learners and should be considered in the design of all online materials.\n\nIdentification of certain characteristics will assist in determining if materials will be fully accessible for all students. Assurance that materials are compliant with the standards, recommendations, and guidelines specified assists educators in the selection and use of accessible versions of materials that can be used with all students, including those with different kinds of challenges and assistive devices.\n\nThe Assurance of Accessibility Standards Rubric does not ask reviewers to make a judgment on the degree of object quality.Instead, it requests that a determination (yes / no) of characteristics be made that, together with assurance of specific Standards, may determine the degree to which the materials are accessible. Only those who feel qualified to make judgments about an object’s accessibility should use this rubric. ";
            // Log Link 8
            LogFile("Solution_Explorer", "Review_Criteria8", richTextBox6.Text);
        }

        // Show lesson solution notes
        private void button19_Click(object sender, EventArgs e)
        {
            richTextBox8.Text = richTextBox9.Text;
            LogFile("Solution_View", "Solution_Original", richTextBox8.Text);
        }

        // Show revised lesson solution built using the results panel
        private void button20_Click(object sender, EventArgs e)
        {
            richTextBox8.Text = richTextBox10.Text;
            LogFile("Solution_View", "Solution_Revised", richTextBox8.Text);
        }

        // Show the example lesson retrieved through the peer comparison system
        private void button21_Click(object sender, EventArgs e)
        {
            richTextBox8.Text = examplepeerlesson;
            LogFile("Solution_View", "Solution_Example", richTextBox8.Text);
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            LogFile("UIButton", "Tutor_Request", button7.Text);
            // Review button click - request support from Amy to assess the quality of the solution
            SolutionExplorerMonitor("review");
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            LogFile("UIButton","Tutor_Request",button5.Text);
            // Suggest button click - request support from Amy to seek and acquire information
            RecommenderSystem(); // Content-Driven Prompts
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            LogFile("UIButton", "Tutor_Request", button6.Text);
            // Assist button click - request support from Amy to construct and refine a solution
            HintDeliverySystem(); // Hint Requests for Help-Seeking Behaviors
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            LogFile("UIButton", "Tutor_Request", button10.Text);
            // Assess button click - request support from Amy to compare the quality of alternative solutions
            PeerComparisonSystem();
        }

        public void RefreshLessonPlan()
        {
            // Update original description of lesson
            richTextBox9.Text = activitydescription;

            // Update properties of the solution - learning activity
            textBox2.Text = UppercaseFirst(activitytype);
            textBox9.Text = activitylabel;
            textBox10.Text = activitysubject;
            textBox11.Text = activitylevel;
            textBox12.Text = activitystandard;
            textBox13.Text = activityurl;

            // Update properties of the solution - technology
            textBox15.Text = techname;
            textBox16.Text = techaffordance;
            textBox17.Text = techsource;
            richTextBox12.Text = techjustification;

            // Update properties of the solution - evaluation criteria
            textBox19.Text = criteria1;
            textBox20.Text = criteria2;
            textBox21.Text = criteria3;
            textBox22.Text = criteria4;
            textBox23.Text = criteria5;
            textBox24.Text = criteria6;
            textBox25.Text = criteria7;
            textBox3.Text = criteria8;

            // Update time properties of the solution
            textBox14.Text = UppercaseFirst(task);
            textBox26.Text = tasktime.ToString() + " min.";
            textBox27.Text = timer1mincount.ToString() + " min.";

            try
            {
                countcheckeditems = 0;
                int counttotalitems;
                string percentagecompletion;
                foreach (int indexChecked in checkedListBox1.CheckedIndices)
                {
                    countcheckeditems++;
                }
                counttotalitems = checkedListBox1.Items.Count;
                percentagecompletion = ((double)countcheckeditems / (double)counttotalitems).ToString("0.00%");
                textBox29.Text = percentagecompletion;
            }
            catch { }

            

        }
        
        // Timer Method to Track Time on Task
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1mincount++;
            textBox27.Text = timer1mincount.ToString() + " min.";

            
                double percentprogress = ((double)timer1mincount / (double)tasktime);
           
                if (percentprogress >= 0.10 && interface_status == "setgoal" && remindprompt1 == "false")
                {
                    LogFile("Tutor", "Amy_TimeManagement", amy_remindsubgoal);
                    AmyDialogBuilder(amy_remindsubgoal);
                    remindprompt1 = "true";
                }
                else if (percentprogress >= 0.50 && interface_status == "informationseeking" && remindprompt2 == "false")
                {
                    LogFile("Tutor", "Amy_TimeManagement", amy_remindinformationseeking);
                    AmyDialogBuilder(amy_remindinformationseeking);
                    remindprompt2 = "true";
                }
                else if (percentprogress >= 0.70 && interface_status == "solutionconstruction" && remindprompt3 == "false")
                {
                    LogFile("Tutor", "Amy_TimeManagement", amy_remindtaskconstruction);
                    AmyDialogBuilder(amy_remindtaskconstruction);
                    remindprompt3 = "true";
                }
                else if (percentprogress >= 0.90 && interface_status == "solutionassessment" && remindprompt4 == "false")
                {
                    LogFile("Tutor", "Amy_TimeManagement", amy_remindsolutionassessment);
                    AmyDialogBuilder(amy_remindsolutionassessment);
                    remindprompt4 = "true";
                }
                else { }
            

        }

        // To display first uppercase letter for dashboard
        public string UppercaseFirst(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }

        // LogFile

        public void LogFile(string UIpanel, string LoggedData1, string LoggedData2)
        {
            string lineSeparator = ((char)0x2028).ToString();
            string paragraphSeparator = ((char)0x2029).ToString();

            string userid = toolStripTextBox1.Text;

            string parsedloggeddata = LoggedData2;
            parsedloggeddata = parsedloggeddata.Replace("\r\n", string.Empty).Replace("\n", string.Empty).Replace("\r", string.Empty).Replace(lineSeparator, string.Empty).Replace(paragraphSeparator, string.Empty);


            StreamWriter logfile;
            logfile = new StreamWriter(Path.GetDirectoryName(Application.StartupPath) + "\\LogFileEvent.txt", true);
            logfile.WriteLine(userid + "';'" + Convert.ToString(Logfileentrycount) + "';'" + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss:ffff") + "';'" + timestampmillisec + "';'" + tabControl1.SelectedIndex.ToString() + "';'"+ UIpanel +"';'"+ LoggedData1 + "';'" + parsedloggeddata+ "';'");
            logfile.Close();
            Logfileentrycount++;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            int xCoordinate = (Cursor.Position.X);
            int yCoordinate = (Cursor.Position.Y);
            LogMouseCursor("Mouse Cursor Movement", xCoordinate.ToString() +"';'"+ yCoordinate.ToString());
        }

        public void LogMouseCursor(string LoggedData1, string LoggedData2)
        {
            string userid = toolStripTextBox1.Text;
            string filename = "nBrowserMouseLog" + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss:ffff") + userid + ".txt";
            StreamWriter logfile2;
            logfile2 = new StreamWriter(Path.GetDirectoryName(Application.StartupPath) + "\\LogFileMouse.txt", true);
            logfile2.WriteLine(userid + "';'" + Convert.ToString(Logfile2entrycount) + "';'" + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss:ffff") + "';'" + timestampmillisec + "';'" + tabControl1.SelectedIndex.ToString() + "';'" + LoggedData1 + "';'" + LoggedData2 + "';'");
            logfile2.Close();
            Logfile2entrycount++;
        }

        public void SpreadingActivation(int matchedelementindex)
        {
            // Call all the functions below the update properties of the network

            // Add the momentum constant value to node weight
            nodeweights[matchedelementindex] += 0.05;

            int dimensionindex = links[matchedelementindex]; // Assign value of the dimension linked to matched element

            // Excitatory path to spread activation to the dimension related to node matched with element - feed forward activation
            dimensions[dimensionindex] = ExcitatoryDimension(matchedelementindex, dimensionindex); // Assign new value calculated for dimension node weight value

            // Loop through all of the nodes corresponding to each dimension
            for (int i = 0; i < dimensions.Length; i++)
            {
                if(dimensionindex != i)
                {
                    // Inhibitory path to spread activation to other dimensions - feed forward activation
                    dimensions[i] = InhibitoryDimension(dimensionindex, i);
                }
                else
                {
                    // Nothing - dont inhibit the value of the dimension itself, only other dimensions
                }
            }

            // Loop through all of the nodes corresponding to each element
            for (int i = 0; i < links.Length; i++)
            {
                if (links[i] == dimensionindex)
                {
                    // Excitatory path to node - backpropagate activation
                    nodeweights[i] = ExcitatoryNode(links[i], i);
                }
                else
                {
                    // Inhibitory path to node - backpropagate activation
                    nodeweights[i] = InhibitoryNode(links[i], i);
                }
            }

            sum_node_weights = 0;
            // Update document weight value (normally this would require looping through each document in an array using a separate link values array as a guide
            for (int i = 0; i < nodeweights.Length; i++)
            {
                sum_node_weights += nodeweights[i];
            }

            documentweight = sum_node_weights / nodeweights.Length;
            LogConsole("Updated node corresponding to document");
        }

        public double ExcitatoryDimension(int indexnode, int indexdimension)
        {
            double newdimensionweight = (dimensions[indexdimension] + 1 / (1 + Math.Exp(-(nodeweights[indexnode] + gain) * excitation))) / 2;
            LogConsole("Feed forward activation through excitatory path");
            return newdimensionweight;
        }

        public double InhibitoryDimension(int indexdimensioninit, int indexdimensionend)
        {
            double newdimensionweight = (dimensions[indexdimensionend] + 1 / (1 + Math.Exp(-(dimensions[indexdimensioninit] + gain) * inhibition))) / 2;
            LogConsole("Feed forward activation through inhibitory path");
            return newdimensionweight;
        }

        //Learner Model - Spreading Activation Algorithm (Backpropagation)
        public double ExcitatoryNode(int indexdimension, int indexnode)
        {
            double newnodeweight = (nodeweights[indexnode] + 1 / (1 + Math.Exp(-(dimensions[indexdimension] + gain) * excitation))) / 2;
            LogConsole("Backpropagate activation through excitatory path");
            return newnodeweight;
        }

        public double InhibitoryNode(int indexdimension, int indexnode)
        {
            double newnodeweight = (nodeweights[indexnode] + 1 / (1 + Math.Exp(-(dimensions[indexdimension] + gain) * inhibition))) / 2;
            LogConsole("Backpropagate activation through inhibitory path");
            return newnodeweight;
        }

        // Run simulated spread of activation
        private void button22_Click(object sender, EventArgs e)
        {
            LogConsole("Start of simulation");
            if(comboBox14.SelectedIndex == 0)
            {
                SpreadingActivation(Convert.ToInt32(numericUpDown2.Value));
                RefreshDevelopmentEnvironment();
                LogConsole("Index match simulation");
            }
            else
            {
                RefreshDevelopmentEnvironment();
                nodematching(richTextBox14.Text);
                findlargestmatch();
                SpreadingActivation(matchedelementindex);
                RefreshDevelopmentEnvironment();
                LogConsole("String match simulation");
            }
            LogConsole("End of simulation");
        }

        // Refresh Developer Tools View
        public void RefreshDevelopmentEnvironment()
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            listBox4.Items.Clear();
            listBox5.Items.Clear();
            

            foreach (string i in elements)
            {
                listBox1.Items.Add(i);
            }
            foreach (string i in urls)
            {
                listBox2.Items.Add(i);
            }
            foreach (double i in nodeweights)
            {
                listBox3.Items.Add(i);
            }
            foreach (int i in links)
            {
                listBox4.Items.Add(i);
            }
            foreach (double i in dimensions)
            {
                listBox5.Items.Add(i);
            }

            textBox18.Text = gain.ToString();
            textBox28.Text = excitation.ToString();
            textBox30.Text = inhibition.ToString();
            textBox31.Text = documentweight.ToString();
            textBox32.Text = momentum.ToString();
            LogConsole("Developer environment is refreshed");
        }

        public void LogConsole(string message)
        {
            richTextBox13.Text += DateTime.Now.ToString("HH:mm:ss tt") + " : " + message + "\n";
        }

        private void button23_Click(object sender, EventArgs e)
        {
            RefreshDevelopmentEnvironment();

        }

        public void findlargestmatch()
        {
            // Find the largest value index number that is not included in a list of prior recommendations
            for (int i = 0; i < listBox6.Items.Count; i++)
            {
                double value = Convert.ToDouble(listBox6.Items[i]);
                if (intlistrecommendations.Contains(i))
                {
                    // Do nothing - it has already been recommended to learner
                }
                else
                {
                    if (value > matchedelementsimilarity)
                    {
                        matchedelementsimilarity = value;
                        matchedelementindex = i;
                    }
                    else
                    {
                        // Do nothing the similarity index is least than a previously matched string and element
                    }
                }
            }
            matchedelementsimilarity = 0;
        }

        public void nodematching(string textstring)
        {
            listBox6.Items.Clear();

            if (listBox1.Items.Count > 0)
            {
                foreach (var listBoxItem in listBox1.Items)
                {
                    string nodestring = listBoxItem.ToString();

                    double distancemetric = ComputeDistance(nodestring, textstring);
                    //MessageBox.Show(Convert.ToString(distancemetric));
                    listBox6.Items.Add(distancemetric.ToString());
                }
            }
            else
            {
                //Catch error
            }
        }

        public double ComputeDistance(string str1, string str2)
        {
            //https://pastebin.com/EfcmR3Xx Implements string comparison algorithm based on character pair similarity
            List<string> pairs1 = WordLetterPairs(str1.ToUpper());
            List<string> pairs2 = WordLetterPairs(str2.ToUpper());

            int intersection = 0;
            int union = pairs1.Count + pairs2.Count;

            for (int i = 0; i < pairs1.Count; i++)
            {
                for (int j = 0; j < pairs2.Count; j++)
                {
                    if (pairs1[i] == pairs2[j])
                    {
                        intersection++;
                        pairs2.RemoveAt(j);//Must remove the match to prevent "GGGG" from appearing to match "GG" with 100% success

                        break;
                    }
                }
            }

            return (2.0 * intersection) / union;
        }

        private List<string> WordLetterPairs(string str)
        {
            List<string> AllPairs = new List<string>();

            // Tokenize the string and put the tokens/words into an array
            string[] Words = Regex.Split(str, @"\s");

            // For each word
            for (int w = 0; w < Words.Length; w++)
            {
                if (!string.IsNullOrEmpty(Words[w]))
                {
                    // Find the pairs of characters
                    String[] PairsInWord = LetterPairs(Words[w]);

                    for (int p = 0; p < PairsInWord.Length; p++)
                    {
                        AllPairs.Add(PairsInWord[p]);
                    }
                }
            }

            return AllPairs;
        }

        private string[] LetterPairs(string str)
        {
            int numPairs = str.Length - 1;

            string[] pairs = new string[numPairs];

            for (int i = 0; i < numPairs; i++)
            {
                pairs[i] = str.Substring(i, 2);
            }

            return pairs;
        }

        private void richTextBox2_Validated(object sender, EventArgs e)
        {
            LogFile("Solution_Explorer", "Activity_Description", richTextBox2.Text);
        }

        public void findlargestweightvalue()
        {
            // Find the largest value index number that is not included in a list of prior recommendations
            for (int i = 0; i < listBox3.Items.Count; i++)
            {
                double value = Convert.ToDouble(listBox3.Items[i]);
                if (intlistrecommendations.Contains(i))
                {
                    // Do nothing - it has already been recommended to learner
                }
                else
                {
                    if (value > largestweightvalue)
                    {
                        largestweightvalue = value;
                        largestweightindex = i;
                    }
                    else
                    {
                        // Do nothing the similarity index is least than a previously matched string and element
                    }
                }
            }
            largestweightvalue = 0;
        }

        public void getdatabase()
        {

            databasestring = "";

            try
            {
                using (StreamReader sr = new StreamReader(Path.GetDirectoryName(Application.StartupPath) + "\\Database.txt"))
                {
                    string line;
                    while((line = sr.ReadLine()) != null)
                    {
                        LogConsole("Retrieved database file: " + line);
                        databasestring = line;
                    }
                }
            }catch(Exception e)
            {
                LogConsole("The database file could not be read.");
            }

            string regexp = @"(\d+(.)\d+)\s+(;)";

            Regex r = new Regex(regexp, RegexOptions.IgnoreCase);

            Match m = r.Match(databasestring);
            int matchCount = 0;
            while (m.Success)
            {
                //for (int i = 1; i <= 2; i++)
                //{
                Group g = m.Groups[1];
                LogConsole("Node Weight Value: " + g);
                //}
                string value = Convert.ToString(g);
                nodeweights[matchCount] = Convert.ToDouble(value);
                m = m.NextMatch();
                LogConsole("Match" + (++matchCount));
            }
            RefreshDevelopmentEnvironment();
        }

        public void setdatabase()
        {

            databasestring = "";

            try
            {
                using (StreamWriter sw = new StreamWriter(Path.GetDirectoryName(Application.StartupPath) + "\\Database.txt"))
                {
                    foreach (double i in nodeweights)
                    {
                        databasestring += i.ToString() + " ; ";
                    }

                    sw.WriteLine(databasestring);
                }
            }catch(Exception e)
            {
                LogConsole("The database file could not be saved.");
            }
        }

        private void getDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            getdatabase();
        }

        private void setDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setdatabase();
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(toolStripComboBox1.SelectedIndex == 0)
            {
                splitContainer6.Panel1Collapsed = false;
                splitContainer6.Panel2Collapsed = true;
            }
            else
            {
                splitContainer6.Panel1Collapsed = true;
                splitContainer6.Panel2Collapsed = false;
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            timestampmillisec++;
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string itemText = checkedListBox1.Items[e.Index].ToString();
            if(e.NewValue == CheckState.Checked)
            {
                LogFile("Tasks", "SubGoal_Met", itemText);
            }
            else
            {
                LogFile("Tasks", "SubGoal_NotMet", itemText);
            }
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            LogFile("Solution_Explorer", "Task_Define", comboBox1.SelectedText);
        }

        private void numericUpDown1_Validated(object sender, EventArgs e)
        {
            LogFile("Solution_Explorer", "Task_Time", Convert.ToString(numericUpDown1.Value));
        }

        private void comboBox3_SelectionChangeCommitted(object sender, EventArgs e)
        {
            LogFile("Solution_Explorer", "Activity_Type", this.comboBox3.Items[this.comboBox3.SelectedIndex].ToString());
        }

        private void textBox4_Validated(object sender, EventArgs e)
        {
            LogFile("Solution_Explorer", "Activity_Label", textBox4.Text);
        }

        private void comboBox5_SelectionChangeCommitted(object sender, EventArgs e)
        {
            LogFile("Solution_Explorer", "Activity_Subject", this.comboBox5.Items[this.comboBox5.SelectedIndex].ToString());
        }

        private void comboBox6_SelectionChangeCommitted(object sender, EventArgs e)
        {
            LogFile("Solution_Explorer", "Activity_Level", this.comboBox6.Items[this.comboBox6.SelectedIndex].ToString());
        }

        private void comboBox7_SelectionChangeCommitted(object sender, EventArgs e)
        {
            LogFile("Solution_Explorer", "Activity_Standard", this.comboBox7.Items[this.comboBox7.SelectedIndex].ToString());
        }

        private void textBox5_Validated(object sender, EventArgs e)
        {
            LogFile("Solution_Explorer", "Activity_URL", textBox5.Text);
        }

        private void textBox6_Validated(object sender, EventArgs e)
        {
            LogFile("Solution_Explorer", "Tech_Name", textBox6.Text);

        }

        private void textBox7_Validated(object sender, EventArgs e)
        {
            LogFile("Solution_Explorer", "Tech_URL", textBox7.Text);

        }

        private void textBox8_Validated(object sender, EventArgs e)
        {
            LogFile("Solution_Explorer", "Tech_Affordance", textBox8.Text);

        }

        private void richTextBox11_Validated(object sender, EventArgs e)
        {
            LogFile("Solution_Explorer", "Tech_Justification", richTextBox11.Text);

        }

        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            LogFile("Solution_Explorer", "Eval_Criteria1", this.comboBox2.Items[this.comboBox2.SelectedIndex].ToString());
        }

        private void comboBox4_SelectionChangeCommitted(object sender, EventArgs e)
        {
            LogFile("Solution_Explorer", "Eval_Criteria2", this.comboBox4.Items[this.comboBox4.SelectedIndex].ToString());
        }

        private void comboBox8_SelectionChangeCommitted(object sender, EventArgs e)
        {
            LogFile("Solution_Explorer", "Eval_Criteria3", this.comboBox8.Items[this.comboBox8.SelectedIndex].ToString());
        }

        private void comboBox9_SelectionChangeCommitted(object sender, EventArgs e)
        {
            LogFile("Solution_Explorer", "Eval_Criteria4", this.comboBox9.Items[this.comboBox9.SelectedIndex].ToString());
        }

        private void comboBox10_SelectionChangeCommitted(object sender, EventArgs e)
        {
            LogFile("Solution_Explorer", "Eval_Criteria5", this.comboBox10.Items[this.comboBox10.SelectedIndex].ToString());
        }

        private void comboBox11_SelectionChangeCommitted(object sender, EventArgs e)
        {
            LogFile("Solution_Explorer", "Eval_Criteria6", this.comboBox11.Items[this.comboBox11.SelectedIndex].ToString());
        }

        private void comboBox12_SelectionChangeCommitted(object sender, EventArgs e)
        {
            LogFile("Solution_Explorer", "Eval_Criteria7", this.comboBox12.Items[this.comboBox12.SelectedIndex].ToString());
        }

        private void comboBox13_SelectionChangeCommitted(object sender, EventArgs e)
        {
            LogFile("Solution_Explorer", "Eval_Criteria8", this.comboBox13.Items[this.comboBox13.SelectedIndex].ToString());
        }

        private void richTextBox10_Validated(object sender, EventArgs e)
        {
            LogFile("Revised_Activity", "Solution_Validated", richTextBox10.Text);
        }

        private void richTextBox10_TextChanged(object sender, EventArgs e)
        {
            LogFile("Revised_Activity", "Solution_Edit", richTextBox10.Text);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(tabControl1.SelectedIndex == 0)
            {
                LogFile("UILayout", "Menu_Selection", "Workspace");
            }
            else
            {
                LogFile("UILayout", "Menu_Selection", "Dashboard");
            }
        }

        private void richTextBox9_TextChanged(object sender, EventArgs e)
        {
            LogFile("Original_Activity", "Activity_Description", richTextBox9.Text);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            LogFile("Solution_Properties", "Activity_Type", textBox2.Text);
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            LogFile("Solution_Properties", "Activity_Label", textBox9.Text);
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            LogFile("Solution_Properties", "Activity_Subject", textBox10.Text);
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            LogFile("Solution_Properties", "Activity_Level", textBox11.Text);
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {
            LogFile("Solution_Properties", "Activity_Standard", textBox12.Text);
        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            LogFile("Solution_Properties", "Activity_URL", textBox13.Text);
        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {
            LogFile("Solution_Properties", "Tech_Name", textBox15.Text);
        }

        private void textBox16_TextChanged(object sender, EventArgs e)
        {
            LogFile("Solution_Properties", "Tech_Affordance", textBox16.Text);
        }

        private void textBox17_TextChanged(object sender, EventArgs e)
        {
            LogFile("Solution_Properties", "Tech_URL", textBox17.Text);
        }

        private void richTextBox12_TextChanged(object sender, EventArgs e)
        {
            LogFile("Solution_Properties", "Tech_Justification", richTextBox12.Text);
        }

        private void textBox19_TextChanged(object sender, EventArgs e)
        {
            LogFile("Solution_Properties", "Eval_Criteria1", textBox19.Text);
        }

        private void textBox20_TextChanged(object sender, EventArgs e)
        {
            LogFile("Solution_Properties", "Eval_Criteria2", textBox20.Text);
        }

        private void textBox21_TextChanged(object sender, EventArgs e)
        {
            LogFile("Solution_Properties", "Eval_Criteria3", textBox21.Text);
        }

        private void textBox22_TextChanged(object sender, EventArgs e)
        {
            LogFile("Solution_Properties", "Eval_Criteria4", textBox22.Text);
        }

        private void textBox23_TextChanged(object sender, EventArgs e)
        {
            LogFile("Solution_Properties", "Eval_Criteria5", textBox23.Text);
        }

        private void textBox24_TextChanged(object sender, EventArgs e)
        {
            LogFile("Solution_Properties", "Eval_Criteria6", textBox24.Text);
        }

        private void textBox25_TextChanged(object sender, EventArgs e)
        {
            LogFile("Solution_Properties", "Eval_Criteria7", textBox25.Text);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            LogFile("Solution_Properties", "Eval_Criteria8", textBox3.Text);
        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {
            LogFile("Task_Completion", "Task_Define", textBox14.Text);
        }

        private void textBox26_TextChanged(object sender, EventArgs e)
        {
            LogFile("Task_Completion", "Task_Time", textBox26.Text);
        }

        private void textBox27_TextChanged(object sender, EventArgs e)
        {
            LogFile("Task_Completion", "Task_TimeActual", textBox27.Text);
        }

        private void textBox29_TextChanged(object sender, EventArgs e)
        {
            LogFile("Task_Completion", "Task_PercentCompletion", textBox29.Text);
        }
    }
}
