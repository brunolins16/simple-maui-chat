﻿namespace MyChatApp;
public partial class MainPage : ContentPage
{
    public MainPage()
	{
		InitializeComponent();
        BindingContext = new ChatViewModel();
    }
}