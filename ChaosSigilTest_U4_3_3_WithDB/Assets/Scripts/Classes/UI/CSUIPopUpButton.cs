using UnityEngine;
using System.Collections;

public class CSUIPopUpButton : CSUIButton {

	public enum ButtonType{
		None,
		Yes,
		No}

	public ButtonType buttonType = ButtonType.None;

	protected override void Start()
	{
		base.Start();

	}

	protected override void OnMouseUpAsButton()
	{
		base.OnMouseUpAsButton();
		transform.parent.GetComponent<CSUIPopUp>().SendMessage("ButtonPressed",(object)buttonType);
	}
}
