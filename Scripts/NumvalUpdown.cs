using Godot;
using System;
using System.Threading.Tasks;

public partial class NumvalUpdown : Control
{
	private Label LabHundreds;
	int hundreds = 0;
	private Label LabTens;
	int Tens = 0;
	private Label LabOnes;
	int Ones = 0;
	bool isUpgrading = false;
	private float timerDelay = 0.1f;
	enum Button
	{
		UpHundred,
		UpTen,
		UpOne,
		DownHundred,
		DownTen,
		DownOne
	}
	private Button button;

	public override void _Ready()
	{
		var container = GetNode<HBoxContainer>("NumContainer");
		LabHundreds = (Label)container.GetChild(0);
		LabTens = (Label)container.GetChild(1);
		LabOnes = (Label)container.GetChild(2);
		LabHundreds.Text = hundreds.ToString(); ;
		LabTens.Text = Tens.ToString();
		LabOnes.Text = Ones.ToString();
	}


	private async void DownBtHundredsUp()
	{
		button = Button.UpHundred;
		GD.Print("up100");
		if (!isUpgrading)
		{
			isUpgrading = true;
			await PerformContinuousUpgrade(button);
		}
	}
	private async void DownBtTensUp()
	{
		button = Button.UpTen;
		GD.Print("up10");
		if (!isUpgrading)
		{
			isUpgrading = true;
			await PerformContinuousUpgrade(button);
		}
	}
	private async void DownBtOnesUp()
	{
		button = Button.UpOne;
		GD.Print("up1");
		if (!isUpgrading)
		{
			isUpgrading = true;
			await PerformContinuousUpgrade(button);
		}
	}
	private async void DownBtHundredsDown()
	{
		button = Button.DownHundred;
		GD.Print("down100");
		if (!isUpgrading)
		{
			isUpgrading = true;
			await PerformContinuousUpgrade(button);
		}
	}
	private async void DownBtTensDown()
	{
		button = Button.DownTen;
		GD.Print("downt10");
		if (!isUpgrading)
		{
			isUpgrading = true;
			await PerformContinuousUpgrade(button);
		}
	}
	private async void DownBtOnesDown()
	{
		button = Button.DownOne;
		GD.Print("down1");
		if (!isUpgrading)
		{
			isUpgrading = true;
			await PerformContinuousUpgrade(button);
		}
	}


	private void UpBt()
	{
		isUpgrading = false;
	}
	private async Task PerformContinuousUpgrade(Button button)
	{
		GD.Print("PerformContinuousUpgrade started");
		while (isUpgrading)
		{
			GD.Print($"Performing action for button: {button}");

			switch (button)
			{
				case Button.UpHundred:
					HundredsUp();
					break;

				case Button.UpTen:
					TensUp();
					break;

				case Button.UpOne:
					OnesUp();
					break;

				case Button.DownHundred:
					HundredsDown();
					break;

				case Button.DownTen:
					TensDown();
					break;

				case Button.DownOne:
					OnesDown();
					break;

			}
			timerDelay = Mathf.Clamp(timerDelay - 0.01f, 0.02f, 0.1f);
			await ToSignal(GetTree().CreateTimer(timerDelay), "timeout");
		}
	}
	private void OnesUp()
	{
		Ones = Ones + 1;
		if (Ones > 9)
		{
			Ones = 0;
			TensUp();
		}
		if (Ones > 0)
		{
			TextureButton Bt = GetNode<TextureButton>("UpArrowContainer/UpBtOnes");
			Bt.Disabled = false;
		}
		LabOnes.Text = Ones.ToString();
	}
	private void TensUp()
	{
		Tens = Tens + 1;
		if (Tens > 9)
		{
			Tens = 0;
			HundredsUp();
		}
		LabTens.Text = Tens.ToString();
	}
	private void HundredsUp()
	{
		hundreds = hundreds + 1;
		if (hundreds > 9)
		{
			hundreds = 9;
			TextureButton Bt = GetNode<TextureButton>("UpArrowContainer/UpBtHund");
			Bt.Disabled = true;
		}
		LabHundreds.Text = hundreds.ToString();
	}
	private void OnesDown()
	{
		Ones = Ones - 1;
		if (Ones < 0)
		{
			Ones = 0;
			TextureButton Bt = GetNode<TextureButton>("UpArrowContainer/UpBtOnes");
			Bt.Disabled = true;

		}
		LabOnes.Text = Ones.ToString();
	}
	private void TensDown()
	{
		Tens = Tens - 1;
		if (Tens < 0)
		{
			if (Ones == 0)
			{
				Tens = 1;
			}
			else
			{
				OnesDown();
				Tens = 0;
			}


		}
		LabTens.Text = Tens.ToString();
	}
	private void HundredsDown()
	{
		hundreds = hundreds - 1;
		if (hundreds < 8)
		{

			TextureButton Bt = GetNode<TextureButton>("UpArrowContainer/UpBtHund");
			Bt.Disabled = false;
		}
		if (hundreds < 0)
		{
			hundreds = 0;
			TensDown();
		}
		LabHundreds.Text = hundreds.ToString();
	}
}
