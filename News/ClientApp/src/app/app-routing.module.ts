import { NgModule } from '@angular/core';
import { Routes, RouterModule, PreloadAllModules } from '@angular/router';
import { Full_ROUTES } from './shared/routes/full-layout.routes';
import { FullLayoutComponent } from './layouts/full/full-layout.component';


const routes: Routes = [
  {
    path: '',
    redirectTo: 'news',
    pathMatch: 'full'
    },
  
    { path: '', component: FullLayoutComponent, data: { title: 'full Views' }, children: Full_ROUTES },
];

@NgModule({
    imports: [RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })],

  exports: [RouterModule]
})
export class AppRoutingModule {}
