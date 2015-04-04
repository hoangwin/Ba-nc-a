using UnityEngine;
using System.Collections;

public class Bonus : MonoBehaviour {

    public GameObject GameBonus;
    public GameObject BonusAnim;
    public GameObject Fish;
    public Animator FishAnim;
    public UILabel TimeLabel;
    public UILabel FishCountLabel;

	// Use this for initialization
    public static int[] _BONUS_ARRAY_FISH_SCALE = {320, 200, 200, 200, 160, 160, 160, 120, 160, 80, 100, 100, 50, 50, 80 };

    public static int[] _BONUS_ARRAY_FISH_TYPE  = {01, 02, 03, 04, 05, 06, 07, 08, 09, 10, 11, 12, 13, 14, 15};
    public static int[] _BONUS_ARRAY_FISH_COUNT = {50, 40, 32, 28, 25, 24, 23, 20, 18, 14, 12, 10, 04, 03, 02 };
    
    public static float _TimeInBonus = 0f;

    public static int _lastTypeCompleted = 0;
    public static int _type = 0;
    public static int _MAX_FISH_COUNT = 0;
    public static int _CURRENT_FISH_COUNT = 0;
    public bool _isBonusPlaying;
    public float _timeShowBonusAnim;

    public static int _MIN_TIME = 30;
    public static int _MAX_TIME = 90;
    public static Bonus instance;
	void Start () {
        instance = this;
        GameBonus.SetActive(false);
        _isBonusPlaying = false;
        _TimeInBonus = Random.Range(_MIN_TIME, _MAX_TIME);
        BonusAnim.SetActive(false);
        _timeShowBonusAnim = 0;
        
	}
    void Update()
    {

        if (_isBonusPlaying)
        {
            _TimeInBonus -= Time.deltaTime;

            TimeLabel.text = ((int)_TimeInBonus) / 60 + ":" + ((int)_TimeInBonus) % 60;// ((int)_TimeInBonus).ToString();
            FishCountLabel.text = _CURRENT_FISH_COUNT.ToString() + "/" + _MAX_FISH_COUNT.ToString();
            checkCompletedBonus();
        } else
        {
            _TimeInBonus -= Time.deltaTime;
            if (_TimeInBonus < 0)
                AddNewBonus();
            if (_timeShowBonusAnim > 0)
            {
                _timeShowBonusAnim -= Time.deltaTime;
                if (_timeShowBonusAnim <= 0)
                {
                    _timeShowBonusAnim = 0;
                    BonusAnim.SetActive(false);
                }

            }
        }

    }
    public void UpdateBonus(int FishType)
    {
        //Debug.Log(FishType +"," + );
        if(_isBonusPlaying)
        {
            if(FishType == _type +1)
            {
                _CURRENT_FISH_COUNT++;
            }


        } 
    }
	

    void AddNewBonus()
    {
        if (ButtonControl.state == 0)
            return;
        GameBonus.SetActive(true);
        _type = Random.Range(0, 15);           

        _TimeInBonus = 120f;
        _lastTypeCompleted = 0;            
        _MAX_FISH_COUNT = _BONUS_ARRAY_FISH_COUNT[_type];
        _CURRENT_FISH_COUNT = 0;
        _isBonusPlaying = true;
        FishAnim.Play("Fish" + (_type+ 1).ToString() +"_Normal");
        Fish.transform.localScale = new Vector3(_BONUS_ARRAY_FISH_SCALE[_type], _BONUS_ARRAY_FISH_SCALE[_type], 1);
        Debug.Log("_type :" +_type);                        
    }
    void checkCompletedBonus()
    {
        if (_TimeInBonus < 0)
        {
            GameBonus.SetActive(false);
            _isBonusPlaying = false;
            _TimeInBonus = Random.Range(_MIN_TIME, _MAX_TIME);
        }
        else if (_CURRENT_FISH_COUNT >= _MAX_FISH_COUNT)
        {
            SoundEngine.instance.PlayOneShot(SoundEngine.instance._soundBonus);
            GameBonus.SetActive(false);
            _isBonusPlaying = false;
            _TimeInBonus = Random.Range(_MIN_TIME, _MAX_TIME);
            BonusAnim.SetActive(true);
            ScoreControl.addCoind(ScoreControl._MAX_COIN_INIT);
            _timeShowBonusAnim = 2;//2 second
            // cong diem o day
        }

            

    }
    public void playAnimAddCoinCompleted()
    {
        Debug.Log("Play Anim Adcoin");
        Bonus.instance.BonusAnim.SetActive(true);
        Bonus.instance._timeShowBonusAnim = 3;//2 second
        Bonus.instance.BonusAnim.GetComponent<Animator>().Play("BonusAnimAddCoin");
    }
	// Update is called once per frame
	
}
