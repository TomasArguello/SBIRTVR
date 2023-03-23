using System.Collections;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SignIn : MonoBehaviour
{
    public string dataReceived;
    public Text NameInput;
    public Text AgeInput;
    public Text EmailInput;
    public Text PasswordInput;
    public Text PasswordConfirmInput;
    public Text LogInEmailInput;
    public Text LogInPasswordInput;
    public GameObject[] GenderOptions;
    public GameObject[] ProOptions;
    public GameObject StartingScreen;
    public GameObject LogInScreen;
    public GameObject SignUpScreen;
    public TouchScreenKeyboard keyboard;

    // Start is called before the first frame update
    void Start()
    {
        if(NameInput == null)
        {
            NameInput = GameObject.Find("Username").GetComponent<Text>();
        }
        if(AgeInput == null)
        {
            AgeInput = GameObject.Find("User Age").GetComponent<Text>();
        }
        if (EmailInput == null)
        {
            EmailInput = GameObject.Find("TAMU Email").GetComponent<Text>();
        }
        if (PasswordInput == null)
        {
            PasswordInput = GameObject.Find("Enter Password").GetComponent<Text>();
        }
        if (PasswordConfirmInput == null)
        {
            PasswordConfirmInput = GameObject.Find("Confirm Password Input").GetComponent<Text>();
        }
        if (GenderOptions == null)
        {
            GenderOptions = new GameObject[3];
            GenderOptions[0] = GameObject.Find("Male");
            GenderOptions[1] = GameObject.Find("Female");
            GenderOptions[0] = GameObject.Find("Unknown");
        }
        if(ProOptions == null)
        {
            ProOptions = new GameObject[5];
            ProOptions[0] = GameObject.Find("Faculty");
            ProOptions[1] = GameObject.Find("Freshman");
            ProOptions[2] = GameObject.Find("Sophomore");
            ProOptions[3] = GameObject.Find("Junior");
            ProOptions[4] = GameObject.Find("Senior");
        }
        ShowKeyboard();
        //SendData("Tomas", "22", "Male", "Senior");
    }

    public void StartGame()
    {
        string name = NameInput.text;
        string age = AgeInput.text;
        string email = EmailInput.text;
        string password1 = PasswordInput.text;
        string password2 = PasswordConfirmInput.text;
        string gender = "";
        string profession = "";
        for(int i = 0; i < GenderOptions.Length; i++)
        {
            if (GenderOptions[i].GetComponent<Toggle>().isOn)
            {
                gender = GenderOptions[i].name;
                break;
            }
        }
        if(gender == "")
        {
            gender = "Unknown";
        }
        for(int i = 0; i < ProOptions.Length; i++)
        {
            if (ProOptions[i].GetComponent<Toggle>().isOn)
            {
                profession = ProOptions[i].name;
                break;
            }
        }
        if (profession == "")
        {
            profession = "Faculty";
        }
        if(name == "" || age == "")
        {
            name = "Player";
            age = "22";
        }
        if(email == "")
        {
            email = "PlayerEmail@email.com";
        }
        if(password1 == "")
        {
            password1 = "password";
        }
        if (password2 == "")
        {
            password2 = "password";
        }
        SignInToSurvey(name, age, email, password1, password2, gender, profession);

    }

    public void LogInToMainScene()
    {
        string email = EmailInput.text;
        string password = PasswordInput.text;

        if(email == "")
        {
            email = "tomasarguello@tamu.edu";
        }
        if(password == "")
        {
            password = "Password";
        }

        StartCoroutine(PostLogInData(email, password));
    }

    public void SwitchToLogInScreen()
    {
        LogInScreen.SetActive(true);
        StartingScreen.SetActive(false);
    }

    public void SwitchToSignUpScreen()
    {
        SignUpScreen.SetActive(true);
        StartingScreen.SetActive(false);
    }

    public void SignInToSurvey(string name,string age, string email, string password1, string password2, string gender, string profession)
    {
        StartCoroutine(PostData(name,age,email,password1,password2,gender,profession));
    }

    IEnumerator PostData(string nameData, string ageData, string emailData, string password1, string password2, string genderData, string professionData)
    {
        string name = nameData;

        WWWForm www = new WWWForm();
        www.AddField("name", name);
        www.AddField("Age", ageData);
        www.AddField("gender", genderData);
        www.AddField("profession", professionData);
        www.AddField("email", emailData);
        www.AddField("password1", password1);
        www.AddField("password2", password2);
        //UnityWebRequest uwr = UnityWebRequest.Post("https://sbirt.softinteraction.com.ngrok.io/survey", www);
        UnityWebRequest uwr = UnityWebRequest.Post("https://sbirt-softinteraction.ngrok.io/survey", www);
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
            dataReceived = "Didn't get shit!";
        }
        else
        {
            dataReceived = uwr.downloadHandler.text;
            Debug.Log("The patient has responded!! He said " + dataReceived);
            SceneManager.LoadScene("MainSceneCharSetup");
        }

    }

    IEnumerator PostLogInData(string emailData, string passwordData)
    {
        WWWForm www = new WWWForm();
        www.AddField("email", emailData);
        www.AddField("password", passwordData);
        UnityWebRequest uwr = UnityWebRequest.Post("https://sbirt-softinteraction.ngrok.io/login", www);
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
            dataReceived = "Didn't get shit!";
        }
        else
        {
            dataReceived = uwr.downloadHandler.text;
            Debug.Log("The patient has responded!! He said " + dataReceived);
            SceneManager.LoadScene("MainSceneCharSetup");
        }
    }

    public void ShowKeyboard()
    {
        keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
    }
}
