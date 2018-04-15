using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour {

    public static PanelManager _inst;
    public Text _Age;
    public Text _Job;
    public Text _Money;


	private void Start()
	{
        if(_inst = null)
        {
            _inst = this;
        }

	}
}
