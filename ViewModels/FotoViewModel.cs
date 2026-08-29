using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace RepasoMAUI.ViewModels
{
    public partial class FotoViewModel : ObservableObject
    {
        [ObservableProperty]
        private string rutaFoto;

        [ObservableProperty]
        private string mensajeEstado;

        [RelayCommand]
        async Task TomarFoto()
        {
            try
            {
                if (!MediaPicker.Default.IsCaptureSupported)
                {
                    MensajeEstado = "Este dispositivo no soporta tomar fotos.";
                    return;
                }

                FileResult foto = await MediaPicker.Default.CapturePhotoAsync();
                if (foto is not null)
                    RutaFoto = await GuardarFotoLocalAsync(foto);
            }
            catch (PermissionException)
            {
                MensajeEstado = "Necesitamos permiso de cámara para continuar. Actívalo en la configuración de la app.";

            }
            catch (FeatureNotSupportedException)
            {
                MensajeEstado = "Este dispositivo no tiene cámara disponible.";
            }
            catch (Exception ex)
            {
                MensajeEstado = $"Ocurrió un error: {ex.Message}";
            }
        }

        [RelayCommand]
        async Task ElegirDeGaleria()
        {
            try
            {
                FileResult foto = await MediaPicker.Default.PickPhotoAsync();
                if (foto is not null)
                    RutaFoto = await GuardarFotoLocalAsync(foto);
            }
            catch (PermissionException)
            {
                MensajeEstado = "Necesitamos permiso para acceder a tus fotos. Actívalo en la configuración de la app.";
            }
            catch (Exception ex)
            {
                MensajeEstado = $"Ocurrió un error: {ex.Message}";
            }
        }

        private async Task<string> GuardarFotoLocalAsync(FileResult foto)
        {
            string rutaLocal = Path.Combine(FileSystem.AppDataDirectory, foto.FileName);

            using Stream sourceStream = await foto.OpenReadAsync();
            using FileStream localStream = File.OpenWrite(rutaLocal);
            await sourceStream.CopyToAsync(localStream);

            MensajeEstado = "Foto guardada correctamente.";
            return rutaLocal;
        }
    }
}
