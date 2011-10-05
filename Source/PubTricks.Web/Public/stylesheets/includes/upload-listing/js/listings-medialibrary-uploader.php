<?php
	// Interpret this as a JavaScript file.
	header("Content-Type:text/javascript");
	
	// Get the path to the root.
	$full_path = __FILE__;
	
	$path_bits = explode( 'wp-content', $full_path );
	
	$url = $path_bits[0];
	
	// Load the WordPress bootstrap. This is relative to the path of this file.
	require_once( $url . '/wp-load.php' );
?>

/*-----------------------------------------------------------------------------------*/
/* WooThemes Media Library-driven AJAX File Uploader Module - JavaScript Functions */
/* 2010-11-05. */
/*
/* The code below is designed to work as a part of the WooThemes Media Library-driven
/* AJAX File Uploader Module. It is included only on screens where this module is used.
/*-----------------------------------------------------------------------------------*/

(function ($) {

  woothemesMLU = {
  
/*-----------------------------------------------------------------------------------*/
/* Remove file when the "remove" button is clicked.
/*-----------------------------------------------------------------------------------*/
  
    removeFile: function () {
     
     $('.mlu_remove').live('click', function(event) { 
        $(this).hide();
        $(this).parents().parents().children('.upload').attr('value', '');
        $(this).parents('.screenshot').slideUp();
        
        return false;
      });
      
      // Hide the delete button on the first row 
      $('a.delete-inline', "#option-1").hide();
      
    }, // End removeFile
    
/*-----------------------------------------------------------------------------------*/
/* Replace the default file upload field with a customised version.
/*-----------------------------------------------------------------------------------*/

    recreateFileField: function () {
    
     // Listings-specific. Adjust the upload field to work with the listing ID.
     var listing_post_id = $('input[name="woo_post_id"]').attr('value');
     
     $('input.upload').attr( 'rel', listing_post_id );
     $('input.upload_button').attr( 'rel', listing_post_id );
    
      $('input.file').each(function(){
        var uploadbutton = '<input class="upload_file_button" type="button" value="Upload" />';
        $(this).wrap('<div class="file_wrap" />');
        $(this).addClass('file').css('opacity', 0); //set to invisible
        $(this).parent().append($('<div class="fake_file" />').append($('<input type="text" class="upload" />').attr('id',$(this).attr('id')+'_file')).val( $(this).val() ).append(uploadbutton));
 
        $(this).bind('change', function() {
          $('#'+$(this).attr('id')+'_file').val($(this).val());
        });
        $(this).bind('mouseout', function() {
          $('#'+$(this).attr('id')+'_file').val($(this).val());
        });
      });
      
    }, // End recreateFileField

/*-----------------------------------------------------------------------------------*/
/* Use a custom function when working with the Media Uploads popup.
/* Requires jQuery, Media Upload and Thickbox JavaScripts.
/*-----------------------------------------------------------------------------------*/

	mediaUpload: function () {
	
	jQuery.noConflict();
	
	$( 'input.upload_button' ).removeAttr('style');
	
      var formfield,
          formID,
          btnContent = true;
      // On Click
      $('input.upload_button').live("click", function () {
        formfield = $(this).prev('input').attr('name');
        formID = $(this).attr('rel');
        
        // Display a custom title for each Thickbox popup.
        var woo_title = '';
        
       if ( $(this).parents('.section').find('.heading') ) { woo_title = $(this).parents('.section').find('.heading').text(); } // End IF Statement
        
        // tb_show( woo_title, 'media-upload.php?post_id='+formID+'&type=image&amp;is_woothemes=yes&amp;woo_title=' + woo_title + '&amp;TB_iframe=1&amp;width=630&amp;height=500' );
        
        tb_show( woo_title, '<?php echo admin_url( 'media-upload.php' ); ?>?is_woothemes_frontend=yes&post_id='+formID+'&type=image&amp;is_woothemes=yes&amp;TB_iframe=1&amp;width=630&amp;height=500' );
        return false;
      });
            
      window.original_send_to_editor = window.send_to_editor;
      window.send_to_editor = function(html) {
        if (formfield) {
        	
          // itemurl = $(html).attr('href'); // Use the URL to the main image.
          itemurl = $(html).find('img').attr('src'); // Use the URL to the size selected.
                   
          var image = /(^.*\.jpg|jpeg|png|gif|ico*)/gi;
          var document = /(^.*\.pdf|doc|docx|ppt|pptx|odt*)/gi;
          var audio = /(^.*\.mp3|m4a|ogg|wav*)/gi;
          var video = /(^.*\.mp4|m4v|mov|wmv|avi|mpg|ogv|3gp|3g2*)/gi;
          
          if (itemurl.match(image)) {
            btnContent = '<img src="'+itemurl+'" alt="" /><a href="#" class="mlu_remove button">Remove Image</a>';
          } else {
            btnContent = '<div class="no_image">'+html+'<a href="#" class="mlu_remove button">Remove</a></div>';
          }
          
          $('#' + formfield).val(itemurl);
          // $('#' + formfield).next().next('div').slideDown().html(btnContent);
          $('#' + formfield).siblings('.screenshot').slideDown().html(btnContent);
          tb_remove();
          
        } else {
          window.original_send_to_editor(html);
        }
        
        // Clear the formfield value so the other media library popups can work as they are meant to. - 2010-11-11.
        formfield = '';
        
      }
      
    } // End mediaUpload
   
  }; // End woothemesMLU Object // Don't remove this, or the sky will fall on your head.

/*-----------------------------------------------------------------------------------*/
/* Execute the above methods in the woothemesMLU object.
/*-----------------------------------------------------------------------------------*/
  
	$(document).ready(function () {

		woothemesMLU.removeFile();
		woothemesMLU.recreateFileField();
		woothemesMLU.mediaUpload();
	
	});
  
})(jQuery);