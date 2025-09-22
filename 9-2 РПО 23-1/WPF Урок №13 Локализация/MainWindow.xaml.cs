using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_Урок__13_Локализация
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /*
         * Реализовать локализацию третьего языка:
         * 
         * Александрова - корейский
         * Антонов      - португальский
         * Духина       - финский
         * Землянский   - датский
         * Золин        - французский
         * Красицкий    - японский
         * Кубанов      - немецкий
         * Лушников     - норвежский
         * Мамонтова    - китайский (мандарин)
         * Метелицин    - итальянский
         * Чавычалов    - испанский
         * Юнусов       - даргинский
         */

        public MainWindow()
        {
            InitializeComponent();
            UpdateUIEng();
        }

        private void radiobtn_Click(object sender, RoutedEventArgs e)
        {
            if(btnEn.IsChecked == true)
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
                UpdateUIEng();
            }
            if(btnRu.IsChecked == true)
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("ru");
                UpdateUIRus();
            }
        }

        private void UpdateUIEng()
        {
            menuFile.Header = StringsEN.MenuFile;
            menuFileNew.Header = StringsEN.MenuFileNew;
            menuFileOpen.Header = StringsEN.MenuFileOpen;
            menuFileSave.Header = StringsEN.MenuFileSave;
            menuFileExit.Header = StringsEN.MenuFileExit;

            menuEdit.Header = StringsEN.MenuEdit;
            menuEditCopy.Header = StringsEN.MenuEditCopy;
            menuEditCut.Header = StringsEN.MenuEditCut;
            menuEditPaste.Header = StringsEN.MenuEditPaste;

            menuHelp.Header = StringsEN.MenuHelp;
            menuHelpFAQ.Header = StringsEN.MenuHelpFAQ;
            menuHelpAbout.Header = StringsEN.MenuHelpAbout;

            menuLeft.Header = StringsEN.MenuLeft;
            menuCenter.Header = StringsEN.MenuCenter;
            menuRight.Header = StringsEN.MenuRight;

            menuBold.Header = StringsEN.MenuBold;
            menuItalic.Header = StringsEN.MenuItalic;
            menuUnderline.Header = StringsEN.MenuUnderline;

            menuBackColor.Header = StringsEN.MenuBackColor;
            menuForeColor.Header = StringsEN.MenuForeColor;

            tbLang.Text = StringsEN.MenuLang;
            tbLocal.Text = StringsEN.Localization;
            this.Title = "Localization ";
            this.Title = this.Title + " " + StringsEN.Localization;
        }

        private void UpdateUIRus()
        {
            menuFile.Header = StringsRU.MenuFile;
            menuFileNew.Header = StringsRU.MenuFileNew;
            menuFileOpen.Header = StringsRU.MenuFileOpen;
            menuFileSave.Header = StringsRU.MenuFileSave;
            menuFileExit.Header = StringsRU.MenuFileExit;

            menuEdit.Header = StringsRU.MenuEdit;
            menuEditCopy.Header = StringsRU.MenuEditCopy;
            menuEditCut.Header = StringsRU.MenuEditCut;
            menuEditPaste.Header = StringsRU.MenuEditPaste;

            menuHelp.Header = StringsRU.MenuHelp;
            menuHelpFAQ.Header = StringsRU.MenuHelpFAQ;
            menuHelpAbout.Header = StringsRU.MenuHelpAbout;

            menuLeft.Header = StringsRU.MenuLeft;
            menuCenter.Header = StringsRU.MenuCenter;
            menuRight.Header = StringsRU.MenuRight;

            menuBold.Header = StringsRU.MenuBold;
            menuItalic.Header = StringsRU.MenuItalic;
            menuUnderline.Header = StringsRU.MenuUnderline;

            menuBackColor.Header = StringsRU.MenuBackColor;
            menuForeColor.Header = StringsRU.MenuForeColor;

            tbLang.Text = StringsRU.MenuLang;
            tbLocal.Text = StringsRU.Localization;
            this.Title = "Локализация ";
            this.Title = this.Title + " " + StringsRU.Localization;
        }
    }
}
