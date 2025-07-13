using UnityEngine;
using UnityEngine.SceneManagement;
public class sceneChanger : MonoBehaviour
{
    public void changeScene(int toSwitch)
    {
        SceneManager.LoadScene(toSwitch);
    }
}
