jQuery.noConflict();
var widthClassOptions = [];
var widthClassOptions = ({
		bestsellers       : 'bestsellers_default_width',
		newproduct       : 'newproduct_default_width',
		related          : 'related_default_width',
		featured         : 'featured_default_width',
		subcategory      : 'subcategory_default_width',
		accessories      : 'grid_default_width',
		additional       : 'additional_default_width',
		productscategory : 'productcategory_default_width',
		manufacturer     : 'manufacturer_default_width'		
});

(function($) { 
	$(".products_block .product-carousel, .product-details .product-carousel, .manufature-main .product-carousel").each(function() {
		var objectID = jQuery(this).attr('id');
		if(widthClassOptions[objectID.replace('-carousel','')])
			var myDefClass= widthClassOptions[objectID.replace('-carousel','')];
		else
			var myDefClass= 'grid_default_width';
		var slider = jQuery(".products_block #" + objectID + ",  .product-details #"+ objectID + ",  .manufature-main #"+ objectID);
		slider.sliderCarousel({
			defWidthClss : myDefClass,
			subElement   : '.slider-item',
			subClass     : 'product-block',
			firstClass   : 'first_item_tm',
			lastClass    : 'last_item_tm',
			slideSpeed : 200,
			paginationSpeed : 800,
			autoPlay : false,
			stopOnHover : false,
			goToFirst : true,
			goToFirstSpeed : 5000,
			goToFirstNav : true,
			pagination : true,
			paginationNumbers: false,
			responsive: true,
			responsiveRefreshRate : 200,
			baseClass : "slider-carousel",
			theme : "slider-theme",
			autoHeight : true
		});
		
		var nextButton = jQuery(this).parent().find('.next');
		var prevButton = jQuery(this).parent().find('.prev');
		nextButton.click(function(){
			slider.trigger('slider.next');
		})
		prevButton.click(function(){
			slider.trigger('slider.prev');
		})
	});
})( jQuery );


function productListAutoSet() { 
	jQuery(".productbox-grid").each(function(){
		var objectID = jQuery(this).attr('id');
		if(objectID.length >0){
			if(widthClassOptions[objectID.replace('-grid','')])
				var myDefClass= widthClassOptions[objectID.replace('-grid','')];
		}else{
			var myDefClass= 'grid_default_width';
		}	
		jQuery(this).smartColumnsRows({
			defWidthClss : myDefClass,
			subElement   : '.product-items',
			subClass     : 'product-block'
		});
	});		
}
jQuery(window).load(function(){productListAutoSet();});
jQuery(window).resize(function(){productListAutoSet();});
(function($) { 
   $(".cart_block").click(function(){
		$(".cart-content").slideToggle('slow');
	}); 
})( jQuery );

(function($) {
	    $(".product-related-products .product-field").wrap('<div class="product-items"><div class="product-block"><div class="product-block-inner"><div class="spacer"></div></div></div></div>');
		 $(".product-related-products .product-field a img ").wrap('<span class="vm-product-media-container"></span>');
		
        $('.main_menu ul li:has(ul)').addClass('parent');
})( jQuery );

(function($) { 
	    $(".navbar-header").click(function(){
			$(".navbar-collapse").slideToggle('slow');
		});
       
})( jQuery );

 

(function($) { 
			
	$(window).scroll(function(){
		if ($(this).scrollTop() > 100) {
			$('.scrollup').fadeIn();
		} else {
			$('.scrollup').fadeOut();
		}
	}); 
	
	$('.scrollup').click(function(){
		$("html, body").animate({ scrollTop: 0 }, 600);
		return false;
	});
 
})( jQuery );

function mobileToggleMenu(){
	
	if (jQuery(window).width() < 980)
	{
		jQuery(".footer-menu .mobile_togglemenu, .sidebar-left .mobile_togglemenu, .sidebar-right .mobile_togglemenu").remove();
		jQuery(".footer-menu h3, .sidebar-left h3, .sidebar-right h3").append( "<a class='mobile_togglemenu'>&nbsp;</a>");
		jQuery(".footer-menu h3, .sidebar-left h3, .sidebar-right h3").addClass('toggle');
		jQuery(".footer-menu .mobile_togglemenu, .sidebar-left .mobile_togglemenu, .sidebar-right .mobile_togglemenu").click(function(){
			jQuery(this).parent().toggleClass('active').parent().find('.custom,.vmgroup, .VMmenu, form, .custom_aboutmefooter,.tagspopular,.custom_social,.custom_paypal').toggle('slow');
		});

	}else{
		jQuery(".footer-menu h3, .sidebar-left h3, .sidebar-right h3").parent().find('ul').removeAttr('style');
		jQuery(".footer-menu h3, .sidebar-left h3, .sidebar-right h3").removeClass('active');
		jQuery(".footer-menu h3, .sidebar-left h3, .sidebar-right h3").removeClass('toggle');
		jQuery(".footer-menu .mobile_togglemenu, .sidebar-left .mobile_togglemenu, .sidebar-right .mobile_togglemenu").remove();
	}
}
jQuery(document).ready(function(){mobileToggleMenu();});
jQuery(window).resize(function(){mobileToggleMenu();});

function menuResponsive(){
	if (jQuery(window).width() <= 979){
		if(jQuery('.main-navigation').hasClass('treeview')!=true){
			jQuery("#menu").addClass('responsive-menu');
			jQuery("#menu").removeClass('wrap-dropdown-multicolumns');
			jQuery("#menu .main-navigation").treeview({
				animated: "slow",
				collapsed: true,
				unique: true		
			});
			jQuery('#menu .main-navigation a.active').parent().removeClass('expandable');
			jQuery('#menu .main-navigation a.active').parent().addClass('collapsable');
			jQuery('#menu .main-navigation .collapsable ul').css('display','block');		
		}
	
	}else{
		jQuery("#menu .hitarea").remove();
		jQuery("#menu").removeClass('responsive-menu');
		jQuery("#menu").addClass('wrap-dropdown-multicolumns');
		jQuery(".main-navigation").removeClass('treeview');
		jQuery("#menu ul").removeAttr('style');
		jQuery('#menu li').removeClass('expandable');
		jQuery('#menu li').removeClass('collapsable');
	}

}
jQuery(document).ready(function(){menuResponsive();});
jQuery(window).resize(function(){menuResponsive();});

(function($) { 
		$(".toggle").treeview({
				animated: 'slow',
				collapsed: true,
				unique: true		
			});
})( jQuery );

function responsivecolumn()
{
if (jQuery(window).width() <= 979)
{
jQuery('#globalContent .row .sidebar-left').appendTo('#globalContent > .row');
}
else if(jQuery(window).width() >= 980)
{
jQuery('#globalContent .row .sidebar-left').prependTo('#globalContent > .row');
}
}
jQuery(document).ready(function(){responsivecolumn();});
jQuery(window).resize(function(){responsivecolumn();});
