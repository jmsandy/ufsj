/**
 * SearchScreenViewModel.java
 * Copyleft 2018 - Universidade Federal de São João del-Rei
 *
 * CONTACT: jmsandy _at_ gmail _dot_ com
 */
package br.edu.ufsj.crawler.webapp.viewmodels;

import java.util.List;
import java.io.IOException;
import org.zkoss.zul.Window;
import org.zkoss.zk.ui.Component;
import org.zkoss.zk.ui.Execution;
import org.zkoss.zk.ui.Executions;
import org.zkoss.bind.annotation.Init;
import org.zkoss.util.resource.Labels;
import org.zkoss.zk.ui.select.Selectors;
import org.zkoss.bind.annotation.Command;
import org.zkoss.bind.annotation.ContextType;
import org.zkoss.zk.ui.select.annotation.Wire;
import org.zkoss.bind.annotation.NotifyChange;
import org.zkoss.bind.annotation.ContextParam;
import org.zkoss.bind.annotation.AfterCompose;
import br.edu.ufsj.crawler.webapp.models.News;
import org.apache.solr.client.solrj.SolrQuery;
import org.apache.solr.client.solrj.SolrClient;
import br.edu.ufsj.crawler.webapp.models.ClientUtils;
import br.edu.ufsj.crawler.webapp.models.NewsResponse;
import org.apache.solr.client.solrj.impl.HttpSolrClient;
import br.edu.ufsj.crawler.webapp.models.OnlyOneVisible;
import org.apache.solr.client.solrj.SolrServerException;
import org.zkoss.zk.ui.select.annotation.VariableResolver;
import org.zkoss.zkplus.spring.DelegatingVariableResolver;
import org.apache.solr.client.solrj.response.QueryResponse;

/**
 * ViewModel responsável por realizar a consulta junto ao SOLR.
 *
 * @author <a href="mailto:jmsandy@gmail.com">José Mauro da Silva Sandy</a>
 * @since 05/12/2018 12:47:09
 * @version $Id$
 */
@VariableResolver(DelegatingVariableResolver.class)
public class SearchScreenViewModel {

    /**
     * Nome da visão.
     */
    private static final String VIEWNAME = SearchScreenViewModel.class.getSimpleName();

    /**
     * URL para acesso ao SOLR. (Mudar para ficar fora da aplicação)
     */
    private static final String URL_SOLR = "http://localhost:8983/solr/crawler_db";

    /**
     * Enum de controle do estado do formulário.
     */
    protected enum ViewModelState {
        SEARCH, DETAIL
    }

    /**
     * Visibilidade da janela.
     */
    protected OnlyOneVisible visibility;
    /**
     * Objeto para consulta ao serviço do SolR.
     */
    protected SolrClient solrClient;
    /**
     * Estado inicial do controller.
     */
    protected ViewModelState state = ViewModelState.DETAIL;
    /**
     * Janela de consulta.
     */
    @Wire
    protected Window searchWindow;
    /**
     * Janela de resultado.
     */
    @Wire
    protected Window detailWindow;
    /**
     * Expressão a ser consultada.
     */
    protected String queryToSearch;
    /**
     * Resposta a consulta realizada.
     */
    private NewsResponse response;

    @Init()
    public void init(@ContextParam(ContextType.EXECUTION) Execution execution) {
        execution.setAttribute(VIEWNAME, this);
        solrClient = new HttpSolrClient.Builder(URL_SOLR).build();
    }

    @AfterCompose
    public void afterCompose(@ContextParam(ContextType.VIEW) Component view,
            @ContextParam(ContextType.EXECUTION) Execution execution) {
        execution.setAttribute(VIEWNAME, this);
        Selectors.wireComponents(view, this, false);

        onShowSearchWindow();
    }

    /**
     * Controle da visibilidade das janelas.
     *
     * @return
     */
    protected OnlyOneVisible getVisibility() {
        if (visibility == null) {
            visibility = new OnlyOneVisible(searchWindow, detailWindow);
        }
        return visibility;
    }

    /**
     * Exibe a janela de consulta.
     */
    @Command
    @NotifyChange("queryToSearch")
    public void onShowSearchWindow() {
        queryToSearch = null;
        state = ViewModelState.DETAIL;
        getVisibility().showOnly(searchWindow);
    }

    /**
     * Realiza a consulta dos dados junto ao SOLR.
     */
    @Command
    @NotifyChange("response")
    public void onSearch() {
        try {
            if (getQueryToSearch() == null || getQueryToSearch().trim().isEmpty()) {
                ClientUtils.showInfoNotification("aviso.termo.pesquisa.nao.informado", new Object[0]);

            } else {
                SolrQuery query = new SolrQuery();
                query.setHighlight(true);
                query.addHighlightField("content");
                query.setQuery(getQueryToSearch());
                query.setStart(0);
                query.setRows(Integer.MAX_VALUE);

                QueryResponse queryResponse = solrClient.query(query);

                List<News> news = queryResponse.getBeans(News.class);

                response = new NewsResponse(getQueryToSearch(), news);

                getVisibility().showOnly(detailWindow);
            }
        } catch (IOException | SolrServerException ex) {
            ClientUtils.showErrorNotification(Labels.getLabel("erro.realizar.pesquisa"));
        }
    }

    /**
     * Abre a tela com as informações de about.
     *
     * @param view visão que acionou a tela de abertura.
     */
    @Command
    public void onOpenAbout(@ContextParam(ContextType.VIEW) Component view) {
        final Window win = (Window) Executions.getCurrent().createComponents(
                "/shared/about.zul", view, null);
        win.doModal();
    }

    /**
     * @return the queryToSearch
     */
    public String getQueryToSearch() {
        return queryToSearch;
    }

    /**
     * @param queryToSearch the queryToSearch to set
     */
    public void setQueryToSearch(String queryToSearch) {
        this.queryToSearch = queryToSearch;
    }

    /**
     * @return the response
     */
    public NewsResponse getResponse() {
        return response;
    }

    /**
     * @param response the response to set
     */
    public void setResponse(NewsResponse response) {
        this.response = response;
    }
}
