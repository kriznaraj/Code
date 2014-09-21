using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BallyTech.UI.Web.Framework;
using BallyTech.Infrastructure.Configuration;

namespace WindowsFormsApplication1
{
    public partial class ViewCommands : Form
    {
        ConfigService configService = null;
        List<CommandActionConfig> viewcommands = new List<CommandActionConfig>();
        BindingSource source = new BindingSource();
        string[] args = null;

        public ViewCommands(string[] args)
        {
            configService = new ConfigService(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
            viewcommands = configService.Get<CommandActionConfig>("CommandActionTypeConfig").ToList();
            InitializeComponent();
            this.args = args.Length == 0 ? new String[] { "Login", "logon", ".cshtml" } : args;
        }


        private void button3_Click(object sender, EventArgs e)
        {
            CommandActionConfig configService = new CommandActionConfig();

            //controllerCreateParam p = new ControllerCreateParam(txtView.Text);
            //foreach (var viewcommand in viewcommands)
            //{
            //    p.Commands.Add(viewcommand);
            //    //configService.Save<CommandParam>(module.Name + "." + p.Name + "." + viewcommand.Name, viewcommand);
            //}
            //module.ControllerCreateParams.Add(p.Name, p);
            // Persist to file
            /*
            FileStream stream = File.Create(ConfigurationSettings.AppSettings["dbPath"] + "\\" + txtModule.Text + "_" + txtView.Text + ".bin");
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, module);
            stream.Close();
            */




        }

        private List<KeyValuePair<string, string>> GetCommandsAssembly()
        {
            List<KeyValuePair<string, string>> commandDlls = new List<KeyValuePair<string, string>>();
            string[] commandDllsPath = Directory.GetFiles(ConfigurationManager.AppSettings["CommandsDllPath"], "BallyTech.UI.Web.Command.*.dll");

            foreach (var item in commandDllsPath)
	        {
		        commandDlls.Add(new KeyValuePair<string, string>(item, item.Substring(item.LastIndexOf("\\") + 1, item.Length - item.LastIndexOf("\\") - 1)));
            }
            return commandDlls;
        }

        private void Add_Click(object sender, EventArgs e)
        {
            Add_Command command = new Add_Command(configService, null);
            command.ShowDialog();
            if (command.CommandActionConfig != null)
            {
                source.Add(command.CommandActionConfig);
                source.ResetCurrentItem();
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Remove_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete the command ?", "Remove Configuration", MessageBoxButtons.YesNo);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                //TODO Code to delete configuration
                source.Remove(listBox1.SelectedItem);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ViewCommands_Load(object sender, EventArgs e)
        {
            if (args != null && args.Length == 3)
            {
                cmbCommandLibrary.DataSource = GetCommandsAssembly();
                cmbCommandLibrary.DisplayMember = "value";
                cmbCommandLibrary.ValueMember = "key";
                cmbCommandLibrary.SelectedIndex = -1;

                source.DataSource = viewcommands;
                txtView.Text = args[1];
                if (!args[2].Equals(".cshtml"))
                {
                    MessageBox.Show("Choose view file and add Command", "Info");
                    this.Close();
                }
                listBox1.DataSource = source;
                listBox1.DisplayMember = "ActionKey";
                listBox1.ValueMember = "ActionKey";
            }
            else
            {
                this.Close();
            }
        }

        private void Edit_Click(object sender, EventArgs e)
        {
            CommandActionConfig commandConfig = this.configService.Get<CommandActionConfig>(typeof(CommandActionConfig).FullName, listBox1.SelectedValue as string);
            Add_Command command = new Add_Command(configService, commandConfig);
            command.ShowDialog();
            if (command.CommandActionConfig != null)
            {
                source.ResetCurrentItem();
            }
        }

        private void cmbCommandLibrary_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            source.DataSource = viewcommands.Where(o => o.CommandConfig.Contains(cmbCommandLibrary.Text.Replace(".dll", "")));
            source.ResetCurrentItem();
        }
    }
}
