import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AlunosComponent } from './components/alunos/alunos.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { PerfilComponent } from './components/perfil/perfil.component';
import { ProfessoresComponent } from './components/professores/professores.component';
import { ProfessoresDetalhesComponent } from './components/professores/professores-detalhes/professores-detalhes.component';

const routes: Routes = [
  {path: 'alunos', component: AlunosComponent},
  {path: 'professores', component: ProfessoresComponent},
  { path: 'alunos/:id', component: AlunosComponent },
  { path: 'professores', component: ProfessoresComponent },
  { path: 'professor/:id', component: ProfessoresDetalhesComponent },
  {path: 'perfil', component: PerfilComponent},
  {path: 'dashboard', component: DashboardComponent},
  {path: '', redirectTo: 'dashboard', pathMatch: 'full'},
  {path: '**', redirectTo: 'dashboard', pathMatch: 'full'}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
