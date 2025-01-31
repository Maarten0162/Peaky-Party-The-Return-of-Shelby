using Godot;
using System;
using System.Threading.Tasks;

public partial class NumvalUpdown : Control
{
	private Label LabHundreds;
	private int Hundreds;
	public int hundreds
	{
		get { return Hundreds; }
		set
		{
			Hundreds = value;

			var upButton = GetNode<TextureButton>("UpArrowContainer/UpBtHund");
			var downButton = GetNode<TextureButton>("DownArrowContainer/DownBtHunds");

			upButton.Disabled = Hundreds == 9;
			downButton.Disabled = Hundreds == 0;
			if (Hundreds == 9 || Hundreds == 0)
			{
				isUpgrading = false;
			}
		}
	}
	private Label LabTens;
	private int Tens;
	public int tens
	{
		get { return Tens; }
		set
		{
			Tens = value;

			var upButton = GetNode<TextureButton>("UpArrowContainer/UpBtTens");
			var downButton = GetNode<TextureButton>("DownArrowContainer/DownBtTens");

			upButton.Disabled = Tens == 9;
			downButton.Disabled = Tens == 0;

			if (Tens == 9 || Tens == 0)
			{
				isUpgrading = false;
			}
		}
	}

	private Label LabOnes;
	private int Ones;
	public int ones
	{
		get { return Ones; }
		set
		{
			Ones = value;

			var upButton = GetNode<TextureButton>("UpArrowContainer/UpBtOnes");
			var downButton = GetNode<TextureButton>("DownArrowContainer/DownBtOnes");

			upButton.Disabled = Ones == 9;
			downButton.Disabled = Ones == 0;

			if (Ones == 9 || Ones == 0)
			{
				isUpgrading = false;
			}
		}
	}
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
		hundreds = 0;
		Tens = 0;
		Ones = 0;
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
		if (isUpgrading)
		{
			
			await PerformContinuousUpgrade(button);
		}
		else
		{
			isUpgrading = true;
			HundredsUp();
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
		ones = ones + 1;

		LabOnes.Text = ones.ToString();
	}
	private void TensUp()
	{
		tens = tens + 1;

		LabTens.Text = tens.ToString();
	}
	private void HundredsUp()
	{
		hundreds = hundreds + 1;

		LabHundreds.Text = hundreds.ToString();
	}
	private void OnesDown()
	{
		ones = ones + -1;

		LabOnes.Text = ones.ToString();
	}
	private void TensDown()
	{
		tens = tens + -1;

		LabTens.Text = tens.ToString();
	}
	private void HundredsDown()
	{
		hundreds = hundreds + -1;

		LabHundreds.Text = hundreds.ToString();
	}
}
