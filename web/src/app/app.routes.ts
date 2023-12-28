import { Routes } from '@angular/router';
import { StatsPageComponent } from './pages/stats-page/stats-page.component';
import { HomePageComponent } from './pages/home-page/home-page.component';

export const routes: Routes = [
    {path: "", component: HomePageComponent},
    {path: "stats", component: StatsPageComponent}
];
