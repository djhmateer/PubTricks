<?php get_header(); ?>
<?php 
// Global query variable
global $wp_query, $woo_options; 
// Get taxonomy query object
$taxonomy_archive_query_obj = $wp_query->get_queried_object();
// Taxonomy term name
$taxonomy_term_nice_name = $taxonomy_archive_query_obj->name;
// Taxonomy term id
$term_id = $taxonomy_archive_query_obj->term_taxonomy_id;
// Get taxonomy object
$taxonomy_short_name = $taxonomy_archive_query_obj->taxonomy;
$taxonomy_raw_obj = get_taxonomy($taxonomy_short_name);
// You can alternate between these labels: name, singular_name
$taxonomy_full_name = $taxonomy_raw_obj->labels->singular_name;
// Get the permalink for the current page.
$taxonomy_permalink = get_term_link( $taxonomy_archive_query_obj, 'woo_video_category' );
?>
    
    <div id="content-wrap">   
    <div id="content" class="col-full">
		<div class="col-left">
		
			<?php if ( $woo_options[ 'woo_breadcrumbs_show' ] == 'true' ) woo_breadcrumbs(); ?>

			<div id="main" class="video-archive">
			<?php if (have_posts()) : $count = 0; ?>
	        
	            <span class="archive_header"><?php echo __( 'Video Category Archives:', 'woothemes' ); ?> <?php echo $taxonomy_term_nice_name; ?></span>
	            
	            <div class="fix"></div>
	            <?php
	            	// Bar displaying video sorting functionality.
	            	echo woo_sorting_bar( $taxonomy_permalink );
	            ?>
	        
	        <?php while (have_posts()) : the_post(); $count++; ?>
	                                                                    
	            <!-- Post Starts -->
	            <div class="post <?php if($count % 3 == 0) { echo "last"; } ?>">
	
	                <?php
	                	if ( $woo_options['woo_post_content'] != 'content' ) {
	                		echo '<a href="' . get_permalink( get_the_ID() ) . '" rel="bookmark" title="' . the_title_attribute( array( 'echo' => 0 ) ) . '">' . woo_image_vimeo('width=' . $woo_options['woo_thumb_w'] . '&height=' . $woo_options['woo_thumb_h'] . '&class=thumbnail&link=img&return=true&id=' . get_the_ID() ) . '</a>';
	                	}
	                ?>
	
	                <h2 class="title"><a href="<?php the_permalink(); ?>" rel="bookmark" title="<?php the_title_attribute(); ?>"><?php the_title(); ?></a></h2>
	                
	                <span class="date"><?php the_time( get_option( 'date_format' ) ); ?></span>
	
	            </div><!-- /.post -->
	            
	            <?php if($count % 3 == 0) { ?>
					<div class="fix"></div>
		        <?php } ?>
	            
	        <?php endwhile; else: ?>
	        
	            <div class="post">
	                <p><?php _e( 'Sorry, no posts matched your criteria.', 'woothemes' ); ?></p>
	            </div><!-- /.post -->
	        
	        <?php endif; ?>  
	    
				<?php woo_pagenav(); ?>
                
			</div><!-- /#main -->

		</div><!-- /.col-left -->

        <?php get_sidebar(); ?>

    </div><!-- /#content -->
	</div><!-- /#content-wrap -->
		
<?php get_footer(); ?>