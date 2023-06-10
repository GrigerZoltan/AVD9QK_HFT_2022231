using AVD9QK_HFT_2022231.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace OperatorDbApp.WpfClient
{

    public class MainWindowViewModel : ObservableRecipient
    {
        public RestCollection<Operator> Operators { get; set; }
        public RestCollection<Faction> Factions { get; set; }
        public RestCollection<Weapon> Weapons { get; set; }


        private Faction selectedFaction;

        public Faction SelectedFaction
        {
            get { return selectedFaction; }
            set
            {
                if (value != null)
                {
                    selectedFaction = new Faction()
                    {
                        FactionName = value.FactionName,
                        Nation = value.Nation,
                        FactionId = value.FactionId
                    };
                    OnPropertyChanged();
                    (DeleteFactionCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        private Operator selectedOperator;

        public Operator SelectedOperator
        {
            get { return selectedOperator; }
            set { if (value != null)
                   {
                    selectedOperator = new Operator()
                    {
                        Name = value.Name,
                        OperatorId = value.OperatorId,
                        Age = value.Age,
                        Height = value.Height,
                        FactionId = value.FactionId,
                        WeaponId = value.WeaponId
                        
                    };
                    OnPropertyChanged();
                    (DeleteOperatorCommand as RelayCommand).NotifyCanExecuteChanged();
                   }
            }
        }

        private Weapon selectedWeapon;

        public Weapon SelectedWeapon
        {
            get { return selectedWeapon; }
            set
            {
                if (value != null)
                {
                    selectedWeapon = new Weapon()
                    {
                        WeaponId = value.WeaponId,
                        WeaponName = value.WeaponName,
                        Caliber = value.Caliber,
                        Facturer = value.Facturer,
                        OperatorId = value.OperatorId
                    };
                    OnPropertyChanged();
                    (DeleteWeaponCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }


        public ICommand CreateFactionCommand { get; set; }

        public ICommand DeleteFactionCommand { get; set; }

        public ICommand UpdateFactionCommand { get; set; }

        public ICommand CreateOperatorCommand { get; set; }

        public ICommand DeleteOperatorCommand { get; set; }

        public ICommand UpdateOperatorCommand { get; set; }

        public ICommand CreateWeaponCommand { get; set; }

        public ICommand DeleteWeaponCommand { get; set; }

        public ICommand UpdateWeaponCommand { get; set; }

        public ICommand UpdateOperatorsPerFactionCommand { get; set; }

        public ICommand UpdateOperatorsPreferredWeapon { get; set; }

        public ICommand UpdateMinHeightPerFaction { get; set; }

        public ICommand UpdateMaxAgePerFaction { get; set; }

        public ICommand UpdateFactionNameWithOperators { get; set; }
        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public ObservableCollection<OperatorsPerFaction> OperatorsPerFaction { get; set; }

        public ObservableCollection<OperatorsPreferredWeapon> OperatorsPreferredWeapon { get; set; }

        public ObservableCollection<MinHeightPerFaction> MinHeightPerFaction { get; set; }

        public ObservableCollection<MaxAgePerFaction> MaxAgePerFaction { get; set; }

        public ObservableCollection<FactionNameWithOperators> FactionNameWithOperators { get; set; }

        WebClient wc;

        public MainWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Operators = new RestCollection<Operator>("http://localhost:55349/", "operator", "hub");
                Factions = new RestCollection<Faction>("http://localhost:55349/", "faction", "hub");
                Weapons = new RestCollection<Weapon>("http://localhost:55349/", "weapon", "hub");

                



                wc = new WebClient();

                var result = wc.DownloadString("http://localhost:55349/stat/operatorsperfaction");
                OperatorsPerFaction = JsonConvert.DeserializeObject<ObservableCollection<OperatorsPerFaction>>(result);

                result = wc.DownloadString("http://localhost:55349/stat/operatorspreferredweapon");
                OperatorsPreferredWeapon = JsonConvert.DeserializeObject<ObservableCollection<OperatorsPreferredWeapon>>(result);

                result = wc.DownloadString("http://localhost:55349/stat/minheightperfaction");
                MinHeightPerFaction = JsonConvert.DeserializeObject<ObservableCollection<MinHeightPerFaction>>(result);

                result = wc.DownloadString("http://localhost:55349/stat/maxageperfaction");
                MaxAgePerFaction = JsonConvert.DeserializeObject<ObservableCollection<MaxAgePerFaction>>(result);

                result = wc.DownloadString("http://localhost:55349/stat/factionnamewithoperators");
                FactionNameWithOperators = JsonConvert.DeserializeObject<ObservableCollection<FactionNameWithOperators>>(result);

                CreateOperatorCommand = new RelayCommand(() =>
                {
                    Operators.Add(new Operator()
                    {
                        Name = SelectedOperator.Name,
                        Age = SelectedOperator.Age,
                        Height = SelectedOperator.Height,
                        FactionId = SelectedOperator.FactionId,
                        WeaponId = SelectedOperator.WeaponId
                    });
                });

                DeleteOperatorCommand = new RelayCommand(() =>
                {
                    Operators.Delete(SelectedOperator.OperatorId);
                },
                () =>
                {
                    return SelectedOperator != null;
                }
                );

                UpdateOperatorCommand = new RelayCommand(() =>
                {
                    Operators.Update(SelectedOperator);
                });


                CreateFactionCommand = new RelayCommand(() =>
                {
                    Factions.Add(new Faction()
                    {
                        FactionName = SelectedFaction.FactionName,
                        Nation = SelectedFaction.Nation
                    });
                });

                DeleteFactionCommand = new RelayCommand(() =>
                {
                    Factions.Delete(SelectedFaction.FactionId);
                },
                () =>
                {
                    return SelectedFaction != null;
                }
                );

                UpdateFactionCommand = new RelayCommand(() =>
                {
                    Factions.Update(SelectedFaction);
                });


                CreateWeaponCommand = new RelayCommand(() =>
                {
                    Weapons.Add(new Weapon()
                    {
                        WeaponName = SelectedWeapon.WeaponName,
                        Caliber = SelectedWeapon.Caliber,
                        Facturer = SelectedWeapon.Facturer,
                        OperatorId = SelectedWeapon.OperatorId
                    });
                });

                DeleteWeaponCommand = new RelayCommand(() =>
                {
                    Weapons.Delete(SelectedWeapon.WeaponId);
                },
                () =>
                {
                    return SelectedWeapon != null;
                }
                );


                UpdateWeaponCommand = new RelayCommand(() =>
                {
                    Weapons.Update(SelectedWeapon);
                });


                UpdateOperatorsPerFactionCommand = new RelayCommand(() =>
                {

                    var result = wc.DownloadString("http://localhost:55349/stat/operatorsperfaction");
                    var newList = JsonConvert.DeserializeObject<List<OperatorsPerFaction>>(result);
                    OperatorsPerFaction.Clear();
                    for (int i = 0; i < newList.Count; i++)
                        OperatorsPerFaction.Add(newList[i]);
                });
                
                UpdateOperatorsPreferredWeapon = new RelayCommand(() =>
                {
                    var result = wc.DownloadString("http://localhost:55349/stat/operatorspreferredweapon");
                    var newList = JsonConvert.DeserializeObject<List<OperatorsPreferredWeapon>>(result);
                    OperatorsPreferredWeapon.Clear();
                    for (int i = 0; i < newList.Count; i++)
                        OperatorsPreferredWeapon.Add(newList[i]);
                });

                UpdateMinHeightPerFaction = new RelayCommand(() =>
                {
                    var result = wc.DownloadString("http://localhost:55349/stat/minheightperfaction");
                    var newList = JsonConvert.DeserializeObject<List<MinHeightPerFaction>>(result);
                    MinHeightPerFaction.Clear();
                    for (int i = 0; i < newList.Count; i++)
                        MinHeightPerFaction.Add(newList[i]);
                });

                UpdateMaxAgePerFaction = new RelayCommand(() =>
                {
                    var result = wc.DownloadString("http://localhost:55349/stat/maxageperfaction");
                    var newList = JsonConvert.DeserializeObject<List<MaxAgePerFaction>>(result);
                    MaxAgePerFaction.Clear();
                    for (int i = 0; i < newList.Count; i++)
                        MaxAgePerFaction.Add(newList[i]);
                });

                UpdateFactionNameWithOperators = new RelayCommand(() =>
                {
                    var result = wc.DownloadString("http://localhost:55349/stat/factionnamewithoperators");
                    var newList = JsonConvert.DeserializeObject<List<FactionNameWithOperators>>(result);
                    FactionNameWithOperators.Clear();
                    for (int i = 0; i < newList.Count; i++)
                        FactionNameWithOperators.Add(newList[i]);
                });

                SelectedFaction = new Faction();
                SelectedOperator = new Operator();
                SelectedWeapon = new Weapon();
                
            }
        }
    }

    public class OperatorsPerFaction
    {
        public string Key { get; set; }
        public int Value { get; set; }
    }

    public class OperatorsPreferredWeapon
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }

    public class MinHeightPerFaction
    {
        public string Key { get; set; }
        public int Value { get; set; }
    }

    public class MaxAgePerFaction
    {
        public string Key { get; set; }
        public int Value { get; set; }
    }

    public class FactionNameWithOperators
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
