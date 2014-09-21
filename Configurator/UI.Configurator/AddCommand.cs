using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BallyTech.UI.Web.Framework;
using BallyTech.Infrastructure.Configuration;
using System.Reflection;
using System.IO;
using System.Configuration;

namespace WindowsFormsApplication1
{
    public enum ResultType
    {
        Json,
        PartialView
    }

    public partial class Add_Command : Form
    {
        public CommandActionConfig CommandActionConfig { get; set; }
        private ConfigService configService;

        public Add_Command(ConfigService configService, CommandActionConfig commandConfig)
        {
            this.configService = configService;
            this.CommandActionConfig = commandConfig;
            InitializeComponent();
            List<CommandConfig> commands = GetCommands();

            cboCommand.DataSource = commands;
            cboCommand.DisplayMember = "CommandKey";
            cboCommand.ValueMember = "CommandUri";
        }

        private List<CommandConfig> GetCommands()
        {
            List<CommandConfig> commands = new List<CommandConfig>();
            string[] commandDlls = Directory.GetFiles(ConfigurationManager.AppSettings["CommandsDllPath"], "BallyTech.UI.Web.Command.*.dll");
            foreach (var item in commandDlls)
            {
                Assembly commandDll = Assembly.LoadFrom(item);
                
                Type[] commandTypes = commandDll.GetTypes();

                foreach (var type in commandTypes)
                {
                    Type commandType = type;
                    while (commandType != null && (commandType.Name != typeof(ProcessCommand<,>).Name && commandType.Name != typeof(ParameterizedActionCommand<>).Name && commandType.Name != typeof(RequestCommand<>).Name && commandType.Name != typeof(ExecutorCommand).Name))
                    {
                        commandType = commandType.BaseType;
                    }

                    if (commandType != null && (commandType.Name == typeof(ProcessCommand<,>).Name || commandType.Name == typeof(ExecutorCommand).Name || commandType.Name == typeof(RequestCommand<>).Name || commandType.Name == typeof(ParameterizedActionCommand<>).Name))
                    {
                        CommandConfig command = new CommandConfig();
                        command.CommandKey = type.FullName;
                        command.CommandUri = type.AssemblyQualifiedName;
                        commands.Add(command);
                    }
                }                
            }

            return commands;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtActionKey.Text == null || txtActionKey.Text == "")
            {
                MessageBox.Show("Please provide a unique action key.", "Add Command", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (cboCommand.SelectedIndex == -1 || cboCommand.SelectedValue == null)
            {
                MessageBox.Show("Please select a command to add.", "Add Command", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (rbView.Checked == false && rbJson.Checked == false)
            {
                MessageBox.Show("Please select result type.", "Add Command", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (rbView.Checked == true && txtViewname.Text == "")
            {
                MessageBox.Show("Please give the output view name.", "Add Command", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.CommandActionConfig = new CommandActionConfig();
            this.CommandActionConfig.ActionKey = txtActionKey.Text;
            this.CommandActionConfig.CommandConfig = cboCommand.Text;
            this.CommandActionConfig.ResultType = rbJson.Checked ? "Json" : "PartialView";
            if (this.CommandActionConfig.ResultType == "PartialView")
            {
                this.CommandActionConfig.ViewName = txtViewname.Text;
                this.CommandActionConfig.RefreshDiv = txtDivId.Text;
            }
            this.configService.Save<CommandActionConfig>(typeof(CommandActionConfig).FullName, this.CommandActionConfig.ActionKey, this.CommandActionConfig);
            this.FindForm().Close();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbJson.Checked == true)
                panel1.Visible = false;
            else
                panel1.Visible = true;
        }

        private void Add_Command_Load(object sender, EventArgs e)
        {
            if (this.CommandActionConfig != null)
            {
                txtActionKey.Enabled = false;
                txtActionKey.Text = this.CommandActionConfig.ActionKey;
                cboCommand.Text = this.CommandActionConfig.CommandConfig;
                if (this.CommandActionConfig.ResultType.ToLower() == "json")
                {
                    rbJson.Checked = true;
                }
                else
                {
                    rbView.Checked = true;
                    txtViewname.Text = this.CommandActionConfig.ViewName;
                    txtDivId.Text = this.CommandActionConfig.RefreshDiv;
                }
            }
            else
            {
                cboCommand.SelectedIndex = -1;
            }
        }


    }
}
