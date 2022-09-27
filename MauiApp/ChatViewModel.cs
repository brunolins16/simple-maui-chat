namespace MyChatApp;

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.SignalR.Client;

internal class ChatViewModel : INotifyPropertyChanged, IAsyncDisposable
{
    private HubConnection _connection;
    public event PropertyChangedEventHandler PropertyChanged;

    public ChatViewModel()
    {
        StartSessionCommand = new AsyncRelayCommand(async () => 
        {
            try
            {
                _connection = await ChatService.Connect();
                _ = _connection.On<string>("messageReceived", (message) => ReceivedMessages.Add(message));

                ReceivedMessages.Add("🥳🥳 Connection Started");

                Connected = true;
                OnPropertyChanged(nameof(Connected));
            }
            catch (Exception ex)
            {
                await Shell.Current.CurrentPage.DisplayAlert("Error", ex.ToString(), "Ok");
            }
            

        }, () => Connected == false);

        SendMessageCommand = new AsyncRelayCommand(() => _connection.SendAsync("SendMessage", Message));
    }

    public bool Connected { get; private set; }
    public ObservableCollection<string> ReceivedMessages { get; } = new ObservableCollection<string>();

    public ICommand StartSessionCommand { get; private init; }
    public ICommand SendMessageCommand { get; private init; }

    public string Message { get; set; }

    private void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    public ValueTask DisposeAsync()
    {
        if (_connection == null)
        {
            return ValueTask.CompletedTask;
        }

        return _connection.DisposeAsync();
    }
}
