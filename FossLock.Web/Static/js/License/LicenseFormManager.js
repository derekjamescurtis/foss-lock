/**
 * <summary>Interaction logic intended for License/create|edit forms. jQuery and sprintf.js must be loaded prior to instantiating this object.</summary>
 * <param name="selectProductSelector"></param>
 * <param name="selectVersionSelector"></param>
 * <param name="selectLocksSelector"></param>
 * <param name="inputOverrideLocksSelector"></param>
 * <param name="productsData">Javascript array containing data about all available Products. <see</param>
 */
function LicenseFormManager(selectProductSelector, selectVersionSelector, selectLocksSelector, inputOverrideLocksSelector, productsData) {
    if (!window.jQuery) throw "jQuery has not been loaded.. nothing good can come of this now.";
    if (!window.sprintf) throw "sprintf.js has not been loaded.. nothing good can come of this now";

    var _productsData = productsData;

    // properties (dom elements)
    this.SelectProductElement = $(selectProductSelector);
    this.SelectVersionElement = $(selectVersionSelector);
    this.SelectLocksElement = $(selectLocksSelector);
    this.OverrideLocksElement = $(inputOverrideLocksSelector);

    // properties (others)
    this.ProductId = function (value) {
        if (value === undefined)
            return SelectProductElement.val();
        else
            SelectProductElement.val(value);
    };
    this.VersionId = function (value) {
        if (value === undefined)
            return SelectVersionElement.val();
        else
            SelectVersionElement.val(value);
    };
    this.UserSetsLocks = function (userSetsLocks) {
        OverrideLocksElement.prop("checked", userSetsLocks);
        SelectLocksElement.prop("disabled", !userSetsLocks);

        // if the user isn't manually setting locks, we reset the
        // default locks for the current product
        if (!userSetsLocks) {
            var product = GetProductData(ProductId());
            if (product) SetLocks(product.DefaultLocks);
        }
    }

    // methods
    this.GetProductData = function (productId) {
        var product = null;

        for (var i = 0; i < _productsData.length; i++) {
            var candidateProduct = _productsData[i];

            if (candidateProduct.Id == productId) {
                product = candidateProduct;
                break;
            }
        }
        return product;
    };
    this.PopulateProducts = function () {
        var htmlString = "<option value=''>Select a Product</option>";
        for (var i = 0; i < _productsData.length; i++) {
            var product = _productsData[i];
            htmlString += sprintf("<option value='%s'>%s</option>",
                                    product.Id,
                                    product.Name);
        }
        SelectProductElement.html(htmlString);

        // manually raise the on-products-changed event
        OnProductChanged();
    }
    this.PopulateVersions = function (productId) {
        var htmlString = "<option value=''>Select a Version</option>";
        var product = GetProductData(productId);
        if (product) {
            for (var i = 0; i < product.Versions.length; i++) {
                var version = product.Versions[i];
                htmlString += sprintf("<option value='%s'>%s</option>",
                                        version.Id, version.Version);
            }
        }
        else {
            htmlString = "<option value=''>---</option>";
        }
        SelectVersionElement.html(htmlString);
    }
    this.SetLocks = function (locks) {
        // unselect everything first
        SelectLocksElement.val(null);

        // reselect the appropriate values
        if (locks) {
            for (var i = 0; i < locks.length; i++) {
                var lock = locks[i];
                var optSelector = sprintf('option[value="%s"]', lock.Value);
                var opt = SelectLocksElement.find(optSelector);
                opt.attr('selected', true);
            }
        }
    };

    // event handler
    this.OnProductChanged = function () {
        var product = GetProductData(ProductId());

        if (product) {
            // we want to make sure we [re]enable this.. see else block below
            OverrideLocksElement.attr('disabled', false);

            PopulateVersions(product.Id);
            UserSetsLocks(false);
        }
        else {
            // disable this.. because if the user messes with it, it'll get reset when they set a product
            OverrideLocksElement.attr('disabled', true);

            // clear our versions and locks
            PopulateVersions(undefined);
            SetLocks(undefined);
            OnOverrideLocksChanged();
        }
    };
    this.SelectProductElement.on("change", this.OnProductChanged);

    // event handler
    this.OnOverrideLocksChanged = function () {
        var isChecked = OverrideLocksElement.prop("checked");
        UserSetsLocks(isChecked);
    }
    this.OverrideLocksElement.on("change", this.OnOverrideLocksChanged);

    return this;
}
