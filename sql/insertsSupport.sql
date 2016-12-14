/// <summary>
        ///   Get method to retrieve a certain set of products given a hint
        /// </summary>
        /// <returns> Product with the correspondind id </returns>
        [Route("api/product/search/category/{hint}")]
        [HttpGet]
        public List<SFA_REST.Lib_Primavera.Model.Product> ListProductsByHint(string hint)
        {
            return Lib_Primavera.PriIntegration.ListProductsByHint(hint);
        }

        /// <summary>
        ///   Get method to retrieve a certain set of products given a its category
        /// </summary>
        /// <returns> Product with the correspondind id </returns>
        [Route("api/product/category/{id}")]
        [HttpGet]
        public List<SFA_REST.Lib_Primavera.Model.Product> GetProductsByCategory(string id)
        {
            return Lib_Primavera.PriIntegration.GetProductsByCategory(id);
        }

        /// <summary>
        ///   Get method to retrieve a certain set of products given its subCategory
        /// </summary>
        /// <returns> Product with the correspondind id </returns>
        [Route("api/product/subcategory/{id}")]
        [HttpGet]
        public List<SFA_REST.Lib_Primavera.Model.Product> GetProductsBySubCategory(string id)
        {
            return Lib_Primavera.PriIntegration.GetProductsBySubCategory(id);
        }