﻿namespace People;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
		// TODO: Initialize the PersonRepository property with the PersonRespository singleton object

	}

	protected override Window CreateWindow(IActivationState activationState)
	{
		return new Window(new AppShell());
	}
}