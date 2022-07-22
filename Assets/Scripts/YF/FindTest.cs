using UnityEngine;
using UnityEngine.UI;

public class FindTest : MonoBehaviour
{
    public GameObject go;//物体
    public GameObject targetGo;//目标物体

    public Image labelUI_img;//标记UI的Image组件

    private float labelUI_HalfWidth;//标记UI一半的宽
    private float labelUI_HalfHeight;//标记UI一半的高
    private Vector2 v1 = new Vector2(0,0);//v3的坐标
    private Vector2 v2 = new Vector2(0, 0);//v3的坐标
    private Vector2 v3 = new Vector2(0, 0);//v3的坐标
    private Vector2 v4 = new Vector2(0, 0);//v4的坐标

    private void Awake()
    {
        labelUI_HalfWidth = labelUI_img.GetComponent<RectTransform>().rect.width / 2;
        labelUI_HalfHeight = labelUI_img.GetComponent<RectTransform>().rect.height / 2;
    }

    private void Update()
    {
        //判断标记UI的显示与隐藏
        if (IsInView(targetGo.transform.position))
        {
            labelUI_img.enabled = false;
        }
        else
        {
            labelUI_img.enabled = true;
        }

        //设置标记UI的显示位置
        GetV3AndV4(out v3, out v4);
        labelUI_img.transform.localPosition = CalculateCrossPoint(go.transform.position, targetGo.transform.position, v3, v4);
    }

    /// <summary>
    /// 得到v3，v4的值
    /// </summary>
    private void GetV3AndV4(out Vector2 v3, out Vector2 v4)
    {
         v1 = Camera.main.WorldToScreenPoint(go.transform.position);
         v2 = Camera.main.WorldToScreenPoint(targetGo.transform.position);
        Debug.Log(targetGo.transform.position+"XXX"+v2);

        Vector2 offset = v2 - v1;
        if (Mathf.Abs(offset.x) > Mathf.Abs(offset.y))
        {
            if (offset.x > 0)
            {
                v3 = new Vector2(Screen.width - labelUI_HalfWidth, 0);
                v4 = new Vector2(Screen.width - labelUI_HalfWidth, Screen.height);
                labelUI_img.transform.rotation = Quaternion.Euler(0, 0, -135);
            }
            else
            {
                v3 = new Vector2(0 + labelUI_HalfWidth, 0);
                v4 = new Vector2(0 + labelUI_HalfWidth, Screen.height);
                labelUI_img.transform.rotation = Quaternion.Euler(0, 0, 45);
            }
        }
        else
        {
            if (offset.y > 0)
            {
                v3 = new Vector2(0, Screen.height - labelUI_HalfHeight);
                v4 = new Vector2(Screen.width, Screen.height - labelUI_HalfHeight);
                labelUI_img.transform.rotation = Quaternion.Euler(0, 0, -40);
            }
            else
            {
                v3 = new Vector2(0, 0 + labelUI_HalfHeight);
                v4 = new Vector2(Screen.width, 0 + labelUI_HalfHeight);
                labelUI_img.transform.rotation = Quaternion.Euler(0, 0, -220);
            }
        }
    }

    /// <summary>
    /// 给定四个点，求两条线段的交点（y=a1x+b1，y=a2x+b2）
    /// v1,v2是物体与目标物体的位置，v3,v4是屏幕左上，左下，右上，右下中的两个点
    /// </summary>
    private Vector2 CalculateCrossPoint(Vector3 v1, Vector3 v2, Vector2 v3, Vector2 v4)
    {
        v1 = Camera.main.WorldToScreenPoint(v1);
        v2 = Camera.main.WorldToScreenPoint(v2);

        float a1 = 0, b1 = 0, a2 = 0, b2 = 0;
        Vector2 crossPoint = Vector2.zero;

        if (v1.x != v2.x)
        {
            a1 = (v1.y - v2.y) / (v1.x - v2.x);
        }
        if (v1.y != v2.y)
        {
            b1 = v1.y - v1.x * (v1.y - v2.y) / (v1.x - v2.x);
        }

        if (v3.x != v4.x)
        {
            a2 = (v3.y - v4.y) / (v3.x - v4.x);
        }
        if (v3.y != v4.y)
        {
            b2 = v3.y - v3.x * (v3.y - v4.y) / (v3.x - v4.x);
        }

        if (a1 == a2 && b1 == b2)
        {
            Debug.LogWarning("两条线共线，没有交点");
            return Vector2.zero;
        }
        else if (a1 == a2)
        {
            Debug.LogWarning("两条线平行，没有交点");
            return Vector2.zero;
        }
        else
        {
            //交点的x和y坐标
            //（v3与v4需要进行特殊判定处理）
            float x = 0;
            float y = 0;
            if (v3.x == v4.x)
            {
                if (v3.x == 0)
                {
                    x = 0;
                    y = b1;
                }
                else
                {
                    x = v3.x;
                    y = x * a1 + b1;
                }
            }
            else if (v3.y == v4.y)
            {
                if (v3.y == 0)
                {
                    y = 0;
                    x = -b1 / a1;
                }
                else
                {
                    y = v3.y;
                    x = (y - b1) / a1;
                }
            }
            else
            {
                x = (b2 - b1) / (a1 - a2);
                y = a1 * (b2 - b1) / (a1 - a2) + b1;
            }

            //限定x和y的范围
            x = Mathf.Clamp(x, 0, Screen.width);
            y = Mathf.Clamp(y, 0, Screen.height);

            crossPoint = new Vector2(x, y);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(GameObject.Find("Canvas").GetComponent<RectTransform>(),
                crossPoint, null, out crossPoint);
            return crossPoint;
        }
    }

    /// <summary>
    /// 是否在指定相机的视野范围内
    /// </summary>
    /// <param name="worldPos">物体世界坐标</param>
    /// <returns>是否在相机视野范围内</returns>
    public bool IsInView(Vector3 worldPos)
    {
        Transform camTransform = Camera.main.transform;
        Vector2 viewPos = Camera.main.WorldToViewportPoint(worldPos);

        //判断物体是否在相机前面  
        Vector3 dir = (worldPos - camTransform.position).normalized;
        float dot = Vector3.Dot(camTransform.forward, dir);

        if (dot > 0 && viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
