/**
 * News.java
 * Copyleft 2018 - Universidade Federal de São João del-Rei
 *
 * CONTACT: jmsandy _at_ gmail _dot_ com
 */
package br.edu.ufsj.crawler.webapp.models;

import java.util.ArrayList;
import org.apache.solr.client.solrj.beans.Field;

/**
 * Representa a estrutura da notícia recuperada.
 *
 * @author <a href="mailto:jmsandy@gmail.com">José Mauro da Silva Sandy</a>
 * @since 05/12/2018 10:34:22
 * @version $Id$
 */
public class News {

    /**
     * URL da notícia.
     */
    @Field
    private String url;

    /**
     * Domínio de extração.
     */
    @Field
    private String domain;

    /**
     * Conteúdo da notícia.
     */
    @Field
    private ArrayList<String> content;
    
    /**
     * Descrição curta das notícias.
     */
    private String shortDescription;

    /**
     * @return the url
     */
    public String getUrl() {
        return url;
    }

    /**
     * @param url the url to set
     */
    public void setUrl(String url) {
        this.url = url;
    }

    /**
     * @return the domain
     */
    public String getDomain() {
        return domain;
    }

    /**
     * @param domain the domain to set
     */
    public void setDomain(String domain) {
        this.domain = domain;
    }

    /**
     * @return the content
     */
    public ArrayList<String> getContent() {
        return content;
    }

    /**
     * @param content the content to set
     */
    public void setContent(ArrayList<String> content) {
        this.content = content;
    }

    /**
     * @return the shortDescription
     */
    public String getShortDescription() {
        if (getContent() != null && getContent().size() > 0) {
            String description = getContent().get(0);
            
            if (description != null && description.length() > 200) {
                shortDescription = description.substring(0, 200) + "...";
                
            } else {
                shortDescription = description + "...";;
            }
        }
        return shortDescription;
    }
}
