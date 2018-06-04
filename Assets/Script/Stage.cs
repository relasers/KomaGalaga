using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour {
    
    List<Dictionary<string, object>> data;
    public int WidthCell = 8;
    public int HeightCell = 5;

    // Use this for initialization
    void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public object GetData(int X, int Y)
    {
        return data[Y][X.ToString()];
    }

    public void ReadData(string filename)
    {

        data = CSVReader.Read(filename);

        //for (int i = 0; i < data.Count ; ++i)
        //{
        //    for(int j = 0; j < 7 ;++j)
        //        Debug.Log("Index :: " + i.ToString() + ":" + data[i][j.ToString()] );
        //}
    }
}
