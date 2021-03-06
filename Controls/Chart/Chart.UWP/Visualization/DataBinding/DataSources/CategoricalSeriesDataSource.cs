﻿using Telerik.Charting;
using Telerik.Core;
using Windows.Foundation;

namespace Telerik.UI.Xaml.Controls.Chart
{
    internal class CategoricalSeriesDataSource : CategoricalSeriesDataSourceBase
    {
        private DataPointBinding valueBinding;

        public DataPointBinding ValueBinding
        {
            get
            {
                return this.valueBinding;
            }
            set
            {
                if (this.valueBinding == value)
                {
                    return;
                }

                this.valueBinding = value;
                this.valueBinding.PropertyChanged += this.OnBoundItemPropertyChanged;

                if (this.ItemsSource != null)
                {
                    this.Rebind(false, null);
                }
            }
        }

        protected override DataPoint CreateDataPoint()
        {
            return new CategoricalDataPoint();
        }

        protected override void InitializeBinding(DataPointBindingEntry binding)
        {
            if (this.valueBinding != null)
            {
                object value = this.valueBinding.GetValue(binding.DataItem);
                double doubleValue;
                if (NumericConverter.TryConvertToDouble(value, out doubleValue))
                {
                    (binding.DataPoint as CategoricalDataPoint).Value = doubleValue;
                }
            }

            base.InitializeBinding(binding);
        }

        protected override void ProcessDouble(DataPoint point, double value)
        {
            (point as CategoricalDataPoint).Value = value;
        }

        protected override void ProcessDoubleArray(DataPoint point, double[] values)
        {
            if (values.Length > 0)
            {
                (point as CategoricalDataPoint).Value = values[0];
            }
        }

        protected override void ProcessNullableDoubleArray(DataPoint point, double?[] values)
        {
            if (values.Length > 0 && values[0].HasValue)
            {
                (point as CategoricalDataPoint).Value = values[0].Value;
            }
        }

        protected override void ProcessSize(DataPoint point, Size size)
        {
            (point as CategoricalDataPoint).Value = size.Width;
        }

        protected override void ProcessPoint(DataPoint dataPoint, Point point)
        {
            (dataPoint as CategoricalDataPoint).Value = point.X;
        }
    }
}
