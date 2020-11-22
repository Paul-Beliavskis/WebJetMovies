import ListItemText from '@material-ui/core/ListItemText';
import ListItemAvatar from '@material-ui/core/ListItemAvatar';
import Avatar from '@material-ui/core/Avatar';
import Typography from '@material-ui/core/Typography';
import ListItem from '@material-ui/core/ListItem';
import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import { faFilm } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";


const MovieListItem = (props) => {

const classes = useStyles();

return (
    <ListItem alignItems="flex-start">
    <ListItemAvatar>
      <Avatar variant="rounded" src={props.movie.poster} className={classes.large}>
        <FontAwesomeIcon icon={faFilm}></FontAwesomeIcon>
      </Avatar>
    </ListItemAvatar>
    <ListItemText
      primary={props.movie.title}
      secondary={
        <React.Fragment>
          <Typography
            component="span"
            variant="body2"
            className={classes.inline}
            color="textPrimary"
          >
           {props.movie.description}
          </Typography>
          <Typography
            component="span"
            variant="body2"
            className={classes.rightAlign}
            color="textPrimary"
          >
           {props.movie.price}
          </Typography>
        </React.Fragment>
      }
    />
  </ListItem> 
);

};

const useStyles = makeStyles((theme) => ({
    inline: {
      display: 'inline'
    },
    rightAlign: {
      fontSize: '24px',
      display: 'inline',
      marginLeft: '400px'
    }
  }));


export default MovieListItem;