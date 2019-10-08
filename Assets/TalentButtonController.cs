using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalentButtonController : MonoBehaviour
{
    public TalentObject talentObj;

    public Text text;

    public Text rankText;

    public Image image;

    public LineRenderer line;

    public Material material;


    // Start is called before the first frame update
    void Start()
    {
        if (talentObj != null)
        {
            setTalent(talentObj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateButton()
    {
        rankText.text = talentObj.Rank + "/" + talentObj.MaxRank;
        if (talentObj.Prereq.Rank == talentObj.Prereq.MaxRank)
        {
            line.startColor = Color.green;
            line.endColor = Color.green;
        }
    }

    public void setTalent(TalentObject talent)
    {
        gameObject.transform.Translate(talent.Pos);
        if (talent.Prereq != null)
            DrawLine(talent.Pos, talent.Prereq.Pos, Color.white, 0);
        image.sprite = talent.Icon;
        text.text = talent.Name;
    }

    void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0.2f)
    {
        GameObject myLine = new GameObject();
        myLine.transform.parent = gameObject.transform.parent;
        myLine.transform.localPosition = new Vector3(0, 0, 0);
        
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.useWorldSpace = false;
        lr.material = new Material(material);

        lr.startColor = color;
        lr.endColor = color;

        lr.startWidth = 0.1f;
        lr.endWidth = 0.1f;

        lr.SetPosition(0, start);
        lr.SetPosition(1, end);

        line = lr;

        if (duration != 0)
        {
            GameObject.Destroy(myLine, duration);
        }
    }

    public void OnClick()
    {
        GetComponentInParent<TalentPanelScript>().BuyTalent(talentObj);
    }
}
