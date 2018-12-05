/**
 * NewsResponse.java
 * Copyleft 2018 - Universidade Federal de São João del-Rei
 *
 * CONTACT: jmsandy _at_ gmail _dot_ com
 */
package br.edu.ufsj.crawler.webapp.models;

import java.util.List;
import org.zkoss.util.resource.Labels;

/**
 * Retorno das consultas.
 *
 * @author <a href="mailto:jmsandy@gmail.com">José Mauro da Silva Sandy</a>
 * @since 05/12/2018 15:25:02
 * @version $Id$
 */
public class NewsResponse {

    /**
     * Consulta executada.
     */
    private final String query;

    /**
     * Lista de notícias.
     */
    private final List<News> news;

    /**
     * Mensagem associada as mensagens.
     */
    private String sizeNewsMessage;

    /**
     * Cria a resposta realizada junto ao SolR.
     *
     * @param query query realizada.
     * @param news notícias recuperadas.
     */
    public NewsResponse(String query, List<News> news) {
        this.news = news;
        this.query = query;
    }

    /**
     * Quantidade de notícias recuperadas.
     *
     * @return quantidade de notícias recuperadas.
     */
    public int sizeNews() {
        return news != null ? news.size() : 0;
    }

    /**
     * @return the query
     */
    public String getQuery() {
        return query;
    }

    /**
     * @return the news
     */
    public List<News> getNews() {
        return news;
    }

    /**
     * @return the sizeNewsMessage
     */
    public String getSizeNewsMessage() {
        return Labels.getLabel("label.numero.resultados", new Object[]{sizeNews()});
    }
}
