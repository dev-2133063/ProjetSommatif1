﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
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
using WpfApp1.ViewModel;

namespace WpfApp1.View
{
    /// <summary>
    /// Logique d'interaction pour Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        public Login()
        {
            InitializeComponent();
            UpdateUI();
        }

        public void UpdateUI()
        {
            labelNomUtilisateur.Content = Ressources.Ressources.labelNomUtilisateur;
            labelMotDePasse.Content = Ressources.Ressources.labelMotDePasse;
            btnConnecter.Content = Ressources.Ressources.btnConnecter;
        }
    }
}
