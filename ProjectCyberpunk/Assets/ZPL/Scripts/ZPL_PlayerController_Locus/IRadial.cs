using UnityEngine;
//射线激光弹接口
public interface IRadial { 
        void Show(Vector3 shootPosition, Vector3 mousePosition);
        void Check();
}
