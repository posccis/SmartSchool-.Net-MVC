import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Subject, takeUntil } from 'rxjs';
import { Disciplina } from 'src/app/models/Disciplina';
import { Professor } from 'src/app/models/Professor';
import { ProfessorService } from 'src/app/services/professor.service';
import { Util } from 'src/app/util/util';

@Component({
  selector: 'app-professores',
  templateUrl: './professores.component.html',
  styleUrls: ['./professores.component.css']
})
export class ProfessoresComponent implements OnInit, OnDestroy {

  public titulo = 'Professores';
  public professorSelecionado: Professor;
  private unsubscriber = new Subject();

  public professores: Professor[];

  constructor(
    private router: Router,
    private professorService: ProfessorService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService) {}

  carregarProfessores() {
    this.spinner.show();
    this.professorService.getAll()
      .pipe(takeUntil(this.unsubscriber))
      .subscribe((professores: Professor[]) => {
        this.professores = professores;
        this.toastr.success('Professores foram carregado com Sucesso!');
      }, (error: any) => {
        this.toastr.error('Professores não carregados!');
        console.log(error);
      }, () => this.spinner.hide()
    );
  }

  ngOnInit() {
    this.carregarProfessores();
  }

  ngOnDestroy(): void {
    this.unsubscriber.next(null);
    this.unsubscriber.complete();
  }

  disciplinaConcat(disciplinas: Disciplina[]) {
    return Util.nomeConcat(disciplinas);
  }
}
