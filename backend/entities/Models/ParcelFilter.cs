using System;
using System.Collections.Generic;
using Pims.Core.Extensions;

namespace Pims.Dal.Entities.Models
{
    /// <summary>
    /// ParcelFilter class, provides a model for filtering parcel queries.
    /// </summary>
    public class ParcelFilter : PropertyFilter
    {
        #region Properties
        /// <summary>
        /// get/set - The parcel municipality.
        /// </summary>
        public string Municipality { get; set; }

        /// <summary>
        /// get/set - The parcel zoning.
        /// </summary>
        public string Zoning { get; set; }

        /// <summary>
        /// get/set - The parcel potential zoning.
        /// </summary>
        public string ZoningPotential { get; set; }

        /// <summary>
        /// get/set - Parcel minimum land area.
        /// </summary>
        /// <value></value>
        public float? MinLandArea { get; set; }

        /// <summary>
        /// get/set - Parcel maximum land area.
        /// </summary>
        /// <value></value>
        public float? MaxLandArea { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of a ParcelFilter class.
        /// </summary>
        public ParcelFilter() { }

        /// <summary>
        /// Creates a new instance of a ParcelFilter class, initializes it with the specified arguments.
        /// </summary>
        /// <param name="neLat"></param>
        /// <param name="neLong"></param>
        /// <param name="swLat"></param>
        /// <param name="swLong"></param>
        public ParcelFilter(double neLat, double neLong, double swLat, double swLong) : base(neLat, neLong, swLat, swLong)
        {
        }

        /// <summary>
        /// Creates a new instance of a ParcelFilter class, initializes it with the specified arguments.
        /// </summary>
        /// <param name="address"></param>
        /// <param name="agencyId"></param>
        /// <param name="statusId"></param>
        /// <param name="classificationId"></param>
        /// <param name="minLandArea"></param>
        /// <param name="maxLandArea"></param>
        /// <param name="minEstimatedValue"></param>
        /// <param name="maxEstimatedValue"></param>
        /// <param name="minAssessedValue"></param>
        /// <param name="maxAssessedValue"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public ParcelFilter(string address, int? agencyId, int? statusId, int? classificationId, float? minLandArea, float? maxLandArea, float? minEstimatedValue, float? maxEstimatedValue, float? minAssessedValue, float? maxAssessedValue, string[] sort)
            : base(address, agencyId, statusId, classificationId, minEstimatedValue, maxEstimatedValue, minAssessedValue, maxAssessedValue, sort)
        {
            this.MinLandArea = minLandArea;
            this.MaxLandArea = maxLandArea;
        }

        /// <summary>
        /// Creates a new instance of a ParcelFilter class, initializes it with the specified arguments.
        /// Extracts the properties from the query string to generate the filter.
        /// </summary>
        /// <param name="query"></param>
        public ParcelFilter(Dictionary<string, Microsoft.Extensions.Primitives.StringValues> query) : base(query)
        {
            // We want case-insensitive query parameter properties.
            var filter = new Dictionary<string, Microsoft.Extensions.Primitives.StringValues>(query, StringComparer.OrdinalIgnoreCase);
            this.Municipality = filter.GetStringValue(nameof(this.Municipality));
            this.Zoning = filter.GetStringValue(nameof(this.Zoning));
            this.ZoningPotential = filter.GetStringValue(nameof(this.ZoningPotential));
            this.MinLandArea = filter.GetFloatNullValue(nameof(this.MinLandArea));
            this.MaxLandArea = filter.GetFloatNullValue(nameof(this.MaxLandArea));
        }
        #endregion

        #region Methods
        /// <summary>
        /// Determine if a valid filter was provided.
        /// </summary>
        /// <returns></returns>
        public override bool ValidFilter()
        {
            return base.ValidFilter()
                || !String.IsNullOrWhiteSpace(this.Municipality)
                || !String.IsNullOrWhiteSpace(this.Zoning)
                || !String.IsNullOrWhiteSpace(this.ZoningPotential)
                || this.MinLandArea.HasValue
                || this.MaxLandArea.HasValue;
        }
        #endregion
    }
}