namespace Шобака
{
    partial class MainMenu
    {
        /// <Summmary>
        /// Обязательная переменная конструктора.
        /// </Summmary>
        private System.ComponentModel.IContainer components = null;

        /// <Summmary>
        /// Освободить все используемые ресурсы.
        /// </Summmary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <Summmary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </Summmary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.справочникToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.основнойСчетToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.мОЛToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.планСчетовToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.поставщикToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.складToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.журналОперацийToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.жерналПроводокToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.справочникToolStripMenuItem,
            this.журналОперацийToolStripMenuItem,
            this.жерналПроводокToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 33);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // справочникToolStripMenuItem
            // 
            this.справочникToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.основнойСчетToolStripMenuItem,
            this.мОЛToolStripMenuItem,
            this.планСчетовToolStripMenuItem,
            this.поставщикToolStripMenuItem,
            this.складToolStripMenuItem});
            this.справочникToolStripMenuItem.Name = "справочникToolStripMenuItem";
            this.справочникToolStripMenuItem.Size = new System.Drawing.Size(139, 29);
            this.справочникToolStripMenuItem.Text = "Справочники";
            // 
            // основнойСчетToolStripMenuItem
            // 
            this.основнойСчетToolStripMenuItem.Name = "основнойСчетToolStripMenuItem";
            this.основнойСчетToolStripMenuItem.Size = new System.Drawing.Size(276, 34);
            this.основнойСчетToolStripMenuItem.Text = "Основные средства";
            this.основнойСчетToolStripMenuItem.Click += new System.EventHandler(this.основнойСчетToolStripMenuItem_Click);
            // 
            // мОЛToolStripMenuItem
            // 
            this.мОЛToolStripMenuItem.Name = "мОЛToolStripMenuItem";
            this.мОЛToolStripMenuItem.Size = new System.Drawing.Size(276, 34);
            this.мОЛToolStripMenuItem.Text = "МОЛ";
            this.мОЛToolStripMenuItem.Click += new System.EventHandler(this.мОЛToolStripMenuItem_Click);
            // 
            // планСчетовToolStripMenuItem
            // 
            this.планСчетовToolStripMenuItem.Name = "планСчетовToolStripMenuItem";
            this.планСчетовToolStripMenuItem.Size = new System.Drawing.Size(276, 34);
            this.планСчетовToolStripMenuItem.Text = "План счетов";
            this.планСчетовToolStripMenuItem.Click += new System.EventHandler(this.планСчетовToolStripMenuItem_Click);
            // 
            // поставщикToolStripMenuItem
            // 
            this.поставщикToolStripMenuItem.Name = "поставщикToolStripMenuItem";
            this.поставщикToolStripMenuItem.Size = new System.Drawing.Size(276, 34);
            this.поставщикToolStripMenuItem.Text = "Поставщик";
            this.поставщикToolStripMenuItem.Click += new System.EventHandler(this.поставщикToolStripMenuItem_Click);
            // 
            // складToolStripMenuItem
            // 
            this.складToolStripMenuItem.Name = "складToolStripMenuItem";
            this.складToolStripMenuItem.Size = new System.Drawing.Size(276, 34);
            this.складToolStripMenuItem.Text = "Склад";
            this.складToolStripMenuItem.Click += new System.EventHandler(this.складToolStripMenuItem_Click);
            // 
            // журналОперацийToolStripMenuItem
            // 
            this.журналОперацийToolStripMenuItem.Name = "журналОперацийToolStripMenuItem";
            this.журналОперацийToolStripMenuItem.Size = new System.Drawing.Size(178, 29);
            this.журналОперацийToolStripMenuItem.Text = "Журнал операций";
            this.журналОперацийToolStripMenuItem.Click += new System.EventHandler(this.журналОперацийToolStripMenuItem_Click);
            // 
            // жерналПроводокToolStripMenuItem
            // 
            this.жерналПроводокToolStripMenuItem.Name = "жерналПроводокToolStripMenuItem";
            this.жерналПроводокToolStripMenuItem.Size = new System.Drawing.Size(180, 29);
            this.жерналПроводокToolStripMenuItem.Text = "Жернал проводок";
            this.жерналПроводокToolStripMenuItem.Click += new System.EventHandler(this.жерналПроводокToolStripMenuItem_Click);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainMenu";
            this.Text = "MainMenu";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem справочникToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem основнойСчетToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem мОЛToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem планСчетовToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem поставщикToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem складToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem журналОперацийToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem жерналПроводокToolStripMenuItem;
    }
}

