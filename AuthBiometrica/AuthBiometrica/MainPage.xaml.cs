using Plugin.Fingerprint;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AuthBiometrica
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Autenticar(object sender, EventArgs e)
        {
            Application.Current.Properties["user_id "] = "3"; //salva o id do usuário
            await Application.Current.SavePropertiesAsync();

            //verifica se o dispositivo possui leitor biométrico
            var result = await CrossFingerprint.Current.IsAvailableAsync(true);

            //se tiver leitor biométrico e o user_id salvo for igual a 3
            if (result && Application.Current.Properties["user_id "].ToString() == "3")
            {
                //ativa o sensor
                var auth = await CrossFingerprint.Current.AuthenticateAsync("Toque no sensor");

                //se der certo...
                _ = auth.Authenticated == true ? Resultado.Text = "Autenticado com sucesso! :)" : Resultado.Text = "Impressão digital não reconhecida";


                
            }
            else
            {
                await DisplayAlert("Ops", "Dispositivo não suportado", "OK");
            }
        }
    }
}
