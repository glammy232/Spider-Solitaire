/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for Android (C) 2019 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

using System;
using UnityEngine;
using YandexMobileAds;
using YandexMobileAds.Base;

public class YandexInter : MonoBehaviour
{
    public static YandexInter YI;
    private String message = "";

    private Interstitial interstitial;
    public void RequestInterstitial()
    {
        MobileAds.SetAgeRestrictedUser(true);

        string adUnitId = "R-M-2521272-2";

        if (this.interstitial != null)
        {
            this.interstitial.Destroy();
        }

        this.interstitial = new Interstitial(adUnitId);

        this.interstitial.OnInterstitialLoaded += this.HandleInterstitialLoaded;
        this.interstitial.OnInterstitialFailedToLoad += this.HandleInterstitialFailedToLoad;
        this.interstitial.OnReturnedToApplication += this.HandleReturnedToApplication;
        this.interstitial.OnLeftApplication += this.HandleLeftApplication;
        this.interstitial.OnAdClicked += this.HandleAdClicked;
        this.interstitial.OnInterstitialShown += this.HandleInterstitialShown;
        this.interstitial.OnInterstitialDismissed += this.HandleInterstitialDismissed;
        this.interstitial.OnImpression += this.HandleImpression;
        this.interstitial.OnInterstitialFailedToShow += this.HandleInterstitialFailedToShow;

        this.interstitial.LoadAd(this.CreateAdRequest());
        this.DisplayMessage(message);
    }

    public void ShowInterstitial()
    {
        this.interstitial.Show();
    }
    public void Start()
    {
        RequestInterstitial();
        ShowInterstitial();
    }

    private AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder().Build();
    }

    private void DisplayMessage(String message)
    {
    }

    #region Interstitial callback handlers

    public void HandleInterstitialLoaded(object sender, EventArgs args)
    {
        this.DisplayMessage("HandleInterstitialLoaded event received");
    }

    public void HandleInterstitialFailedToLoad(object sender, AdFailureEventArgs args)
    {
        this.DisplayMessage("HandleInterstitialFailedToLoad event received with message: " + args.Message);
    }

    public void HandleReturnedToApplication(object sender, EventArgs args)
    {
        this.DisplayMessage("HandleReturnedToApplication event received");
    }

    public void HandleLeftApplication(object sender, EventArgs args)
    {
        this.DisplayMessage("HandleLeftApplication event received");
    }

    public void HandleAdClicked(object sender, EventArgs args)
    {
        this.DisplayMessage("HandleAdClicked event received");
    }

    public void HandleInterstitialShown(object sender, EventArgs args)
    {
        this.DisplayMessage("HandleInterstitialShown event received");
    }

    public void HandleInterstitialDismissed(object sender, EventArgs args)
    {
        this.DisplayMessage("HandleInterstitialDismissed event received");
    }

    public void HandleImpression(object sender, ImpressionData impressionData)
    {
        var data = impressionData == null ? "null" : impressionData.rawData;
        this.DisplayMessage("HandleImpression event received with data: " + data);
    }

    public void HandleInterstitialFailedToShow(object sender, AdFailureEventArgs args)
    {
        this.DisplayMessage("HandleInterstitialFailedToShow event received with message: " + args.Message);
    }

    #endregion
}