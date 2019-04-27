using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LD44
{
    public class GameplayUIManager : MonoBehaviour
    {
        public void Init()
        {
            UpdateCurrentChamacosLabel(0);
            UpdateRestingChamacosLabel(0);
            UpdateWorkingChamacosLabel(0);
            UpdateReadyChamacosLabel(0);
            UpdateDaysLabel(0);
            UpdateTicksLabel();
            UpdateEnergyFactoryLabel(0);
            UpdateEnergyChunkFactoryLabel(0);
            UpdateEnergyShipLabel(0);
            UpdateEnergyAlienLabel(0);

            GameManager.Instance.OnCurrentChamacosModified += UpdateCurrentChamacosLabel;
            GameManager.Instance.OnReadyChamacosModified += UpdateReadyChamacosLabel;
            GameManager.Instance.OnRestingChamacosModified += UpdateRestingChamacosLabel;
            GameManager.Instance.OnWorkingChamacosModified += UpdateWorkingChamacosLabel;

            TimeManager.Instance.OnFactoryTicked += UpdateTicksLabel;

            FactoryManager.Instance.OnUpdatedEnergyProduction += UpdateEnergyChunkFactoryLabel;
            FactoryManager.Instance.OnChunkEnergyProduced += UpdateEnergyFactoryLabel;

            ShipManager.Instance.OnEnergyAdded += UpdateEnergyShipLabel;
            AlienManager.Instance.OnEnergyRequest += UpdateEnergyAlienLabel;

            goToWorkButton.onClick.AddListener(GameManager.Instance.SendChamacoToWork);
        }

        public void UpdateCurrentChamacosLabel(int chamacos)
        {
            currentChamacosLabel.text = chamacos.ToString();
        }

        public void UpdateRestingChamacosLabel(int chamacos)
        {
            restingChamacosLabel.text = chamacos.ToString();
        }

        public void UpdateWorkingChamacosLabel(int chamacos)
        {
            workingChamacosLabel.text = chamacos.ToString();
        }

        public void UpdateReadyChamacosLabel(int chamacos)
        {
            readyChamacosLabel.text = chamacos.ToString();
        }

        public void UpdateDaysLabel(int days)
        {
            dayLabel.text = days.ToString();
        }

        public void UpdateTicksLabel()
        {
            tickLabel.text = TimeManager.Instance.CurrentTick.ToString();
        }

        public void UpdateEnergyShipLabel(int quantity)
        {
            energyShipLabel.text = quantity.ToString();
        }

        public void UpdateEnergyFactoryLabel(int quantity)
        {
            energyFactoryLabel.text = quantity.ToString();
        }

        public void UpdateEnergyChunkFactoryLabel(float quantity)
        {
            energyChunkFactoryLabel.text = quantity.ToString();
        }

        public void UpdateEnergyAlienLabel(int quantity)
        {
            energyAlienLabel.text = quantity.ToString();
        }

        [SerializeField] TextMeshProUGUI currentChamacosLabel;
        [SerializeField] TextMeshProUGUI restingChamacosLabel;
        [SerializeField] TextMeshProUGUI workingChamacosLabel;
        [SerializeField] TextMeshProUGUI readyChamacosLabel;
        [SerializeField] TextMeshProUGUI dayLabel;
        [SerializeField] TextMeshProUGUI tickLabel;

        [SerializeField] TextMeshProUGUI energyFactoryLabel;
        [SerializeField] TextMeshProUGUI energyShipLabel;
        [SerializeField] TextMeshProUGUI energyChunkFactoryLabel;
        [SerializeField] TextMeshProUGUI energyAlienLabel;

        [SerializeField] Button goToWorkButton;
    }
}
