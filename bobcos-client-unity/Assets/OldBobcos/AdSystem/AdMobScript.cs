using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.UI;

public class AdMobScript : MonoBehaviour
{
    public static AdMobScript instance;
    public string AppId = "ca-app-pub-3587359981014635~1471434894";
    private BannerView _bv;
    private RewardedAd _ra;
    private InterstitialAd interstitial;

    public void Start()
    {

        instance = this;
        //initiliaze 
        MobileAds.Initialize(AppId);

        RequestRewardedAd();
        RequestInterstitial();

    }
    public void EnteredToMainMenu()
    {
        if (_bv == null)
        {


            RequestBanner();
        }
    }

    public void EnteredToWORLD()
    {
        _bv.Destroy();
    }

   

    private void RequestBanner()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3587359981014635/6340618193";

#else
            string adUnitId = "unexpected_platform";
#endif

        // Create a 320x50 banner at the top of the screen.
        _bv = new BannerView(adUnitId, AdSize.Banner, AdPosition.TopLeft);


        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        _bv.LoadAd(request);

        
    }
    private void RequestInterstitial()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3587359981014635/4921086128";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        string adUnitId = "unexpected_platform";
#endif





        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);
        // Create an empty ad request.

        interstitial.OnAdClosed += Ýnterstitial_OnAdClosed;



        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);
    }

    private void Ýnterstitial_OnAdClosed(object sender, System.EventArgs e)
    {
        interstitial.Destroy();
        RequestInterstitial();
    }

    public void ShowInter()
    {
        if(interstitial.IsLoaded())
        {
            interstitial.Show();
        }
    }




    public void ShowRewardAd()
    {
        if (_ra.IsLoaded())
        {
            _ra.Show();
        }
    }
    public void RequestRewardedAd()
    {
        _ra = new RewardedAd("ca-app-pub-3587359981014635/7261482464");

        // Create an empty ad request.

        _ra.OnAdClosed += _ra_OnAdClosed;
        _ra.OnAdFailedToLoad += _ra_OnAdFailedToLoad;
        _ra.OnAdFailedToShow += _ra_OnAdFailedToShow;
        _ra.OnUserEarnedReward += _ra_OnUserEarnedReward;

        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        _ra.LoadAd(request);
    }

    private void _ra_OnUserEarnedReward(object sender, Reward e)
    {
        ClientSend.SendString("AD_212");
    }

    private void _ra_OnAdFailedToShow(object sender, AdErrorEventArgs e)
    {
        NewChatSystem.newChatSystem.AddChat("<color=red>[ERROR: Error showing ad.]</color>");

    }

    private void _ra_OnAdFailedToLoad(object sender, AdErrorEventArgs e)
    {
        NewChatSystem.newChatSystem.AddChat("<color=red>[ERROR: Error loading ad.]</color>");

    }

    private void _ra_OnAdClosed(object sender, System.EventArgs e)
    {
        RequestRewardedAd();
    }
}
