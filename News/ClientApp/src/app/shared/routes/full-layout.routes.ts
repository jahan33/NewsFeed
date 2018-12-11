
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../../guards/index';
//Route for content layout with sidebar, navbar and footer.


export const Full_ROUTES: Routes = [
    
  {
    path: 'news',
        loadChildren: './news/news.module#NewsModule'
       
    },
    
    
     

];
