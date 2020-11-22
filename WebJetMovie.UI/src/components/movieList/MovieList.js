import { List } from '@material-ui/core';
import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Divider from '@material-ui/core/Divider';
import MovieListItem from './MovieListItem';
import { Circle } from 'react-spinners-css';

const useStyles = makeStyles((theme) => ({
  root: {
    width: '700px',
    backgroundColor: theme.palette.background.paper,
  },
}));

export default function MovieList(props) {
  const classes = useStyles();

  return (
    props.isFetching ? <Circle size={200}/> : <List className={classes.root}>
    {props.movies.map((movie) => 
    <>
      <MovieListItem movie={movie} />
      <Divider variant="inset" component="li" />
      </>
    )}
    </List>
  );
}